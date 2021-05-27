<template>
    <input class="form__input" :value="value" :placeholder="placeholder" :type="type"
           @input="$emit('update:value', $event.target.value)">
</template>

<script>
export default {
    name: "FormInput",
    props: {
        type: String,
        placeholder: String,
        value: String,
    },
    emits: ["update:value"]
}
</script>

<style scoped lang="scss">
.form__input {
    display: block;
    font-size: 1.25rem;
    padding: 0.2rem;
}
</style>