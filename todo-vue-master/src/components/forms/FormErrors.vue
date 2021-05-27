<template>
    <div class="form__errors">
        <small class="title">Please fix following errors:</small>
        <small class="error" v-for="(value, name, index) in errors" :key="name">
            {{ `${index + 1}) ${value}` }}
        </small>
    </div>
</template>

<script>
export default {
    name: "FormErrors",
    props: {
        errors: {
            type: Object,
            required: true
        }
    }
}
</script>

<style scoped lang="scss">
.form__errors {
    color: red;
    margin-right: 3.8rem;

    .title {
        display: block;
    }
}
</style>