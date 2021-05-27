<template>
    <div class="home">
        <h1 class="home__title">Welcome to the todo app</h1>
        <p class="home__description">You can create and manage you todo lists with this app.</p>
        <p class="home__description"> To start using this app login or register new free account</p>
    </div>
</template>

<script>
export default {
    name: 'Home',
}
</script>

<style scoped lang="scss">
.home {
    margin-top: 3rem;
    text-align: center;

    &__description {
        margin-top: 0.5rem;
    }
}
</style>
