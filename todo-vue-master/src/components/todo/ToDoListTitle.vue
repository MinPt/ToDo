<template>
    <div class="toDoListTitle">
        <input type="text" :value="list.name" @input="updateList($event.target.value)">
    </div>
</template>

<script>
import {useStore} from "vuex";

import _ from "lodash";
import {computed} from "vue";

export default {
    name: "ToDoListTitle",
    setup() {
        const store = useStore();

        const list = computed(() => store.state.toDoList.list);
        const updateList = _.debounce(async (newName) => {
            await store.dispatch("toDoList/updateList", newName);
        }, 1000);

        return {
            updateList,
            list
        }
    }
}
</script>

<style scoped lang="scss">

.toDoListTitle {
    display: flex;

    & > input {
        flex: 1;
        margin: 1rem auto 0 auto;
        border: none;
        background: none;

        font-size: 2rem;
        font-weight: bold;

        &:focus {
            outline: none;
        }
    }
}
</style>