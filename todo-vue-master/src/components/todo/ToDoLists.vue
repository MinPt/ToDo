<template>
    <div class="toDoLists">
        <div class="toDoLists__header">
            <div class="searchbar">
                <input class="searchbar__search" type="text" v-model="filter" placeholder="What are you looking for?">
            </div>
        </div>

        <p v-if="isEmpty">Seems there are no lists. But you can always add one!</p>
        <ToDoListPreview v-else class="toDoLists__preview" v-for="list in lists" :key="list.id"
                         :to-do-list="list" @removeList="removeList"/>
        <div class="toDoLists__footer">
            <Btn text="Add new list" @click="createList"/>
        </div>
    </div>
</template>

<script>
import {computed, ref} from "vue";
import {useRouter} from "vue-router";

import _ from "lodash";

import api from "@/gateways/api";

import ToDoListPreview from "@/components/todo/ToDoListPreview";
import Btn from "@/components/forms/Btn";
import FormInput from "@/components/forms/FormInput";

export default {
    name: "ToDoLists",
    components: {FormInput, Btn, ToDoListPreview},
    async setup() {
        const router = useRouter();

        const filter = ref("");
        const toDoLists = ref([]);

        const response = await api.get("todolists");
        toDoLists.value.push(...response.data);

        const removeList = async (id) => {
            await api.delete(`todolists/${id}`);
            _.remove(toDoLists.value, l => l.id === id);
        };

        const createList = async () => {
            const {data} = await api.post(`todolists`, {});
            await router.push({name: "ToDo", params: {id: data.id}});
        };

        const lists = computed(() => {
            return toDoLists.value.filter(l => l.name.toLowerCase().includes(filter.value.toLowerCase()));
        });
        const isEmpty = computed(() => {
            return lists.value.length === 0;
        });

        return {
            filter,
            lists,
            isEmpty,
            removeList,
            createList
        };
    }
}
</script>

<style scoped lang="scss">
.toDoLists {
    margin: 0 auto;
    width: 65%;

    &:first-child {
        border-top: none;
    }

    &__header {
        margin: 1rem 0;

        .searchbar {
            display: flex;

            &__search {
                padding: 0.2rem;
                font-size: 1.1rem;
                flex: 1;
            }
        }
    }

    &__preview {
        margin-top: 1rem;
        margin-bottom: 1rem;
        padding: 1rem;
    }

    &__footer {
        margin-top: 1rem;
        display: flex;
        justify-content: center;
    }
}
</style>