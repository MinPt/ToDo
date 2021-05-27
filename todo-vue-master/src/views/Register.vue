<template>
    <div class="register">
        <h1 class="register__header">Register</h1>
        <form class="register__form form">
            <FormErrors :errors="errors" v-if="errors"/>

            <div class="form__group">
                <p>Email</p>
                <FormInput type="email" placeholder="email" v-model:value="email"/>
            </div>
            <div class="form__group">
                <p>Username</p>
                <FormInput type="text" placeholder="username" v-model:value="username"/>
            </div>
            <div class="form__group">
                <p>Password</p>
                <FormInput type="password" placeholder="password" v-model:value="password"/>
            </div>

            <div class="form__footer">
                Already have an account?
                <router-link class="link" :to="{name: 'Login'}">Login</router-link>
            </div>

            <Btn class="form__button" text="Register" :disabled="registerBtnDisabled" @click="register"/>
        </form>
    </div>
</template>

<script>
import FormInput from "@/components/forms/FormInput";
import Btn from "@/components/forms/Btn";
import {useRouter, onBeforeRouteLeave} from "vue-router";
import {useStore} from "vuex";
import {computed, reactive, toRefs} from "vue";
import FormErrors from "@/components/forms/FormErrors";

export default {
    name: "Register",
    components: {FormErrors, Btn, FormInput},
    setup() {
        const router = useRouter();
        const store = useStore();

        const state = reactive({
            email: "",
            username: "",
            password: "",
            fetching: false
        });

        onBeforeRouteLeave((to, from) => {
            store.commit("auth/setErrors", null);
        });

        const errors = computed(() => store.getters["auth/errors"]);

        const register = async () => {
            state.fetching = true;
            await store.dispatch("auth/register", {...state});
            state.fetching = false;

            if (!errors.value) {
                await router.push({name: "Login"});
            }
        }

        const formValid = computed(() => {
            return state.email !== "" && state.username !== "" && state.password !== "";
        });

        const registerBtnDisabled = computed(() => {
            return !formValid.value || state.fetching;
        });

        return {
            ...toRefs(state),
            errors,
            register,
            registerBtnDisabled
        };
    }
}
</script>

<style scoped lang="scss">
@import "src/styles/form";

.register {
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