<template>
    <Suspense>
        <template #default>
            <ToDoLists/>
        </template>
        <template #fallback>
            <Fallback text="Loading lists"/>
        </template>
    </Suspense>
</template>

<script>
import ToDoLists from "@/components/todo/ToDoLists";
import Spinner from "@/components/common/Spinner";
import Fallback from "@/components/common/Fallback";

export default {
    name: "ToDos",
    components: {Fallback, ToDoLists},
}
</script>