<template>
    <div class="toDoList">
        <div class="toDoList__title">
            <h4>{{ toDoList.name }}</h4>
            <small>Last updated: {{ lastUpdated }}</small>
        </div>

        <div class="buttonGroup">
            <Btn class="toDoList__btn" text="More" :to="{name: 'ToDo', params: {id: toDoList.id}}"/>
            <Btn class="toDoList__btn" :classes="{'toDoList__btn-red':true}" text="Delete"
                 @click="$emit('removeList',toDoList.id)"/>
        </div>
    </div>
</template>

<script>
import Btn from "@/components/forms/Btn";
import toHumanFriendlyDate from "@/composables/toHumanFriendlyDate";

export default {
    name: "ToDoListPreview",
    components: {
        Btn
    },
    props: {
        toDoList: {
            type: Object,
            required: true
        }
    },
    setup(props) {
        const date = toHumanFriendlyDate(props.toDoList.dateChanged);
        const lastUpdated = date === null ? "Never" : date;

        return {
            lastUpdated
        }
    }
}
</script>

<style scoped lang="scss">
.toDoList {
    border-radius: 5px;
    background-color: #6f747b1f;

    &__title {
        display: flex;
        justify-content: space-between;

        small {
            color: #757575;
        }
    }

    .buttonGroup {
        display: flex;
        justify-content: space-between;
    }

    &__btn {
        margin-top: 0.5rem;
        font-size: 1rem;
    }

    &__btn-red {
        &:hover {
            color: #eeeeee;
            background-color: #cf2626;
            cursor: pointer;
        }
    }
}
</style>