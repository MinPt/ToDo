<template>
    <div class="toDoListHeader">
        <p>Created: {{ dateCreated }}</p>
        <p>Last Updated: {{ dateChanged }}</p>
        <div v-if="updating">
            <Spinner/>
            Updating...
        </div>
        <div v-else>
            <i class="fas fa-save"></i>
            Updated
        </div>
    </div>
</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";
import Spinner from "@/components/common/Spinner";

export default {
    name: "ToDoListHeader",
    components: {Spinner},
    setup() {
        const store = useStore();

        const dateCreated = computed(() => store.getters["toDoList/listDateCreated"])
        const dateChanged = computed(() => {
            const date = store.getters["toDoList/listDateChanged"];
            return date === null ? "Never" : date;
        });
        const updating = computed(() => store.state.toDoList.updating);

        return {
            dateCreated,
            dateChanged,
            updating
        };
    }
}
</script>

<style scoped lang="scss">

</style>