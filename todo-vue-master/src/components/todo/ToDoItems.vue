<template>
    <div class="toDoItems">
        <p v-if="isEmpty">It's quite empty here. Let's add some items!</p>
        <ToDoItem v-else v-for="item in items" :key="item.id" :text="item.text" :completed="item.isCompleted"
                  @update="updateToDoItem({id:item.id, toDoListId: item.toDoListId, ...$event})"
                  @delete="deleteToDoItem(item.id)"/>
    </div>
</template>

<script>
import {useStore} from "vuex";
import _ from "lodash";

import ToDoItem from "@/components/todo/ToDoItem";
import {computed} from "vue";

export default {
    name: "ToDoItems",
    components: {ToDoItem},
    setup() {
        const store = useStore();

        const items = computed(() => store.state.toDoList.list.toDoItems);
        const isEmpty = computed(() => store.state.toDoList.list.toDoItems.length === 0);

        const updateToDoItem = _.debounce(async (updatedItem) => {
            await store.dispatch("toDoList/updateToDoItem", updatedItem);
        }, 500);

        const deleteToDoItem = async (itemId) => {
            await store.dispatch("toDoList/deleteToDoItem", itemId);
        };

        return {
            items,
            isEmpty,
            updateToDoItem,
            deleteToDoItem
        };
    }
}
</script>

<style scoped>

</style>