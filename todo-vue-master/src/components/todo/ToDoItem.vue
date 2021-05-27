<template>
    <div class="toDoItem">
        <input class="toDoItem__checkbox" :disabled="!checkboxDisabled" type="checkbox" :checked="completed"
               @input="$emit('update', {isCompleted: $event.target.checked, text})">
        <input :class="{toDoItem__input: true, toDoItem__input_crossed: completed}"
               type="text" :value="text" @input="$emit('update', {isCompleted:completed, text: $event.target.value})">
        <i class="fas fa-trash-alt toDoItem__delete" @click="$emit('delete')"></i>
    </div>
</template>

<script>
import {computed} from "vue";

export default {
    name: "ToDoItem",
    props: {
        completed: {
            type: Boolean,
            required: true,
        },
        text: {
            type: String,
            required: true,
        }
    },
    emits: ["update", "delete"],
    setup(props) {
        const checkboxDisabled = computed(() => !!props.text);

        return {
            checkboxDisabled
        }
    }
}
</script>

<style scoped lang="scss">
.toDoItem {
    display: flex;
    justify-content: center;
    align-items: center;

    &__checkbox {
        margin: 0 5px;
        width: 1.25rem;
        height: 1.25rem;
    }

    &__input {
        width: 100%;
        margin: 0 5px;
        padding: 5px;
        border: none;
        border-bottom: 1px solid #222831;
        background: none;

        font-size: 1.25rem;
        transition: 0.35s ease-in-out;

        &:focus {
            outline: none;
        }
    }

    &__input_crossed {
        text-decoration: line-through;
        opacity: 55%;
    }

    &__delete {
        &:hover {
            cursor: pointer;
        }
    }
}
</style>