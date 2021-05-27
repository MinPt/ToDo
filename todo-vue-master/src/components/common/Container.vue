<template>
    <div class="container">
        <slot></slot>
    </div>
</template>

<script>
export default {
    name: "Container"
}
</script>

<style scoped lang="scss">
.container {
    margin: 0 auto;
    padding: 10px;
    width: 80%;
}
</style>