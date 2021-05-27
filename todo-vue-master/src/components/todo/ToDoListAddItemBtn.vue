<template>
    <div class="toDoListAddItemBtn">
        <Btn text="Add Goal" :disabled="!canAddItem" @click="addToDoItem"/>
    </div>
</template>

<script>
import {useStore} from "vuex";

import Btn from "@/components/forms/Btn";
import {computed} from "vue";

export default {
    name: "ToDoListAddItemBtn",
    components: {Btn},
    setup() {
        const store = useStore();

        const canAddItem = computed(() => store.getters["toDoList/canAddItem"]);
        const addToDoItem = () => {
            store.dispatch("toDoList/addToDoItem");
        };

        return {
            canAddItem,
            addToDoItem
        }
    }
}
</script>

<style scoped lang="scss">
.toDoListAddItemBtn {
    display: flex;
    justify-content: center;
}
</style>