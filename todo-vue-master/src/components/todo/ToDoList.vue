<template>
    <div class="toDoList">
        <ToDoListHeader class="toDoList__header"/>
        <ToDoListTitle class="toDoList__title"/>
        <ToDoItems class="toDoList__items"/>
        <ToDoListAddItemBtn class="toDoList__button"/>

        <Btn text="Back" :to="{name: 'ToDos'}"/>
    </div>
</template>

<script>
import {useRouter} from "vue-router";
import {useStore} from "vuex";

import ToDoItem from "@/components/todo/ToDoItem";
import Btn from "@/components/forms/Btn";
import ToDoItems from "@/components/todo/ToDoItems";
import ToDoListTitle from "@/components/todo/ToDoListTitle";
import ToDoListAddItemBtn from "@/components/todo/ToDoListAddItemBtn";
import ToDoListHeader from "@/components/todo/ToDoListHeader";

export default {
    name: "ToDoList",
    components: {
        ToDoListHeader,
        ToDoListAddItemBtn,
        ToDoListTitle,
        ToDoItems,
        Btn,
        ToDoItem
    },
    props: {
        id: {
            type: String,
            required: true
        }
    },
    async setup(props) {
        const router = useRouter();
        const store = useStore();

        try {
            await store.dispatch("toDoList/fetchList", props.id);
        } catch (ex) {
            await router.push({name: "NotFound"});
        }
    }
}
</script>

<style scoped lang="scss">
.toDoList {
    &__items {
        margin-top: 1.5rem;
    }

    &__button {
        margin-top: 1rem;
    }
}
</style>