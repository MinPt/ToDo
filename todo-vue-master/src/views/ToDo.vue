<template>
    <Suspense>
        <template #default>
            <ToDoList :id="id"/>
        </template>
        <template #fallback>
            <Fallback text="Loading list"/>
        </template>
    </Suspense>
</template>

<script>
import ToDoList from "@/components/todo/ToDoList";
import Fallback from "@/components/common/Fallback";

export default {
    name: "ToDo",
    components: {Fallback, ToDoList},
    props: {
        id: {
            type: String,
            required: true
        }
    }
}
</script>