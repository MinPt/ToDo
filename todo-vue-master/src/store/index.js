import {createStore} from 'vuex'
import auth from "@/store/modules/auth";
import toDoList from "@/store/modules/toDoList";

export default createStore({
    state: () => ({}),
    getters: {},
    mutations: {},
    actions: {},
    modules: {
        auth,
        toDoList
    }
})
