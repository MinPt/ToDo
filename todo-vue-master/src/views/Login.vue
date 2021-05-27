<template>
    <div class="login">
        <h1 class="login__header">Login</h1>
        <form class="login__form form">
            <FormErrors :errors="errors" v-if="errors"/>

            <div class="form__group">
                <p>Email</p>
                <FormInput type="email" placeholder="email" v-model:value="email"/>
            </div>
            <div class="form__group">
                <p>Password</p>
                <FormInput type="password" placeholder="password" v-model:value="password"/>
            </div>
            <div class="form__footer">
                Don't have an account?
                <router-link class="link" :to="{name: 'Register'}">Register</router-link>
            </div>

            <Btn class="form__button" text="Login" :disabled="loginBtnDisabled" @click="login"/>
        </form>
    </div>
</template>

<script>
import {reactive, toRefs, computed} from "vue";
import {onBeforeRouteLeave, useRouter} from "vue-router";
import {useStore} from "vuex";

import Btn from "@/components/forms/Btn";
import FormInput from "@/components/forms/FormInput";
import FormErrors from "@/components/forms/FormErrors";

export default {
    name: "Login",
    components: {FormErrors, FormInput, Btn},
    setup() {
        const router = useRouter();
        const store = useStore();

        const state = reactive({
            email: "",
            password: "",
            fetching: false
        });

        onBeforeRouteLeave((to, from) => {
            store.commit("auth/setErrors", null);
        });

        const errors = computed(() => store.getters["auth/errors"]);

        const login = async () => {
            state.fetching = true;
            await store.dispatch("auth/login", {...state});
            state.fetching = false;

            if (!errors.value) {
                await router.push({name: "Home"});
            }
        }

        const formValid = computed(() => {
            return state.email !== "" && state.password !== "";
        });

        const loginBtnDisabled = computed(() => {
            return !formValid.value || state.fetching;
        });

        return {
            ...toRefs(state),
            errors,
            login,
            loginBtnDisabled
        };
    }
}
</script>

<style scoped lang="scss">
@import "src/styles/form";

.login {
    &__header {
        margin: 1rem 0;
        text-align: center;
    }

    &__form {
        display: flex;
        flex-direction: column;
        align-items: center;
    }
}

.link {
    color: cornflowerblue;
}
</style>
