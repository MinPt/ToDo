<template>
    <div class="footer">
        &copy; Todo inc.
    </div>
</template>

<script>
import {computed} from "vue";

export default {
    name: "Footer",
    setup() {
        const currentYear = computed(() => new Date().getFullYear());
        return {currentYear};
    }
}
</script>

<style scoped lang="scss">
.footer{
    margin: 0.25rem;
}
</style>