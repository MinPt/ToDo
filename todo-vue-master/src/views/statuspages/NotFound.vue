<template>
    <div class="notfound">
        <h1 class="notfound__title">Page not found</h1>
        <p class="notfound__message">Seems like this page doesn't exist</p>

        <Btn class="notfound__button" :to="{name: 'Home'}" text="Go home"/>
    </div>
</template>

<script>

import Btn from "@/components/forms/Btn";

export default {
    name: "NotFound",
    components: {Btn},
}
</script>

<style scoped lang="scss">
.notfound {
    margin-top: 250px;

    display: flex;
    flex-direction: column;
    align-items: center;

    &__message {
        margin: 0.35rem 0;
    }

    &__button {
        margin-top: 0.2rem;
    }
}
</style>