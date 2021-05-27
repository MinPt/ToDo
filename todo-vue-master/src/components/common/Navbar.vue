<template>
    <div class="navbar">
        <nav class="navbar__nav nav">
            <router-link :to="{name: 'Home'}" class="nav__link">Home</router-link>
            <router-link :to="{name: 'ToDos'}" class="nav__link">To Do Lists</router-link>
        </nav>

        <div class="navbar__currentUser currentUser" v-if="isLogged">
            <span class="currentUser__greeting">Hello, {{ user.UserName }} |</span>
            <span class="currentUser__logout" @click="logout">Logout</span>
        </div>
        <nav class="navbar__nav nav" v-else>
            <router-link :to="{name:'Login'}" class="nav__link">Login</router-link>
            <router-link :to="{name:'Register'}" class="nav__link">Register</router-link>
        </nav>
    </div>
</template>

<script>
import {useStore} from "vuex";
import {useRouter} from "vue-router";

import useCurrentUser from "@/composables/useCurrentUser";

export default {
    name: "NavBar",
    setup() {
        const store = useStore();
        const router = useRouter();

        const {isLogged, user} = useCurrentUser();

        const logout = () => {
            store.dispatch("auth/logout");
            router.push({name: "Home"});
        };

        return {
            isLogged,
            user,
            logout
        }
    }
}
</script>

<style scoped lang="scss">
.navbar {
    display: flex;
    align-items: center;
    justify-content: space-between;

    user-select: none;

    padding: 0 1rem;
    height: 3.5rem;
    background-color: #222831;
}

.nav {
    &__link {
        color: #ffffff;
        text-decoration: none;
    }

    &__link:hover {
        color: #d6d6d6;
    }

    &__link:not(:last-child):after {
        content: '|';
        margin: 0 5px;
        color: #ffffff;
    }
}

.currentUser {
    color: #ffffff;

    & > * {
        padding: 0 5px;
    }

    &__logout:hover {
        color: #d6d6d6;
        cursor: pointer;
    }
}
</style>