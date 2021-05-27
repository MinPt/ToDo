import {useStore} from "vuex";
import {computed} from "vue";

export default function useCurrentUser() {
    const store = useStore();

    const isLogged = computed(() => store.getters["auth/isLogged"]);
    const user = computed(() => store.getters["auth/user"]);

    return {
        isLogged,
        user
    }
}