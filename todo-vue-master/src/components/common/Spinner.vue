<template>
    <i class="fas fa-spinner spinner"></i>
</template>

<script>
export default {
    name: "Spinner"
}
</script>

<style scoped lang="scss">
.spinner{
    animation: spin 1.5s linear infinite;
    @keyframes spin {
        100% {
            transform: rotate(360deg);
        }
    }
}
</style>