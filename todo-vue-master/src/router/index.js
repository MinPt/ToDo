import {createRouter, createWebHistory} from "vue-router";
import store from "@/store";

import Home from "../views/Home.vue"
import ToDos from "@/views/ToDos";
import ToDo from "@/views/ToDo";
import NotFound from "@/views/statuspages/NotFound";
import Login from "@/views/Login";
import Register from "@/views/Register";


const routes = [
    {
        path: "/",
        name: "Home",
        component: Home
    },
    {
        path: "/login",
        name: "Login",
        component: Login,
        meta: {guestOnly: true}
    },
    {
        path: "/register",
        name: "Register",
        component: Register,
        meta: {guestOnly: true}
    },
    {
        path: "/toDoLists",
        name: "ToDos",
        component: ToDos,
        meta: {requiresAuth: true}
    },
    {
        path: "/toDoLists/:id",
        name: "ToDo",
        component: ToDo,
        props: true,
        meta: {requiresAuth: true}
    },
    {
        path: "/:pathMatch(.*)",
        redirect: {name: "NotFound"}
    },
    {
        path: "/notfound",
        name: "NotFound",
        component: NotFound
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

router.beforeEach(((to, from) => {
    const userLogged = store.getters["auth/isLogged"];

    if (to.meta.requiresAuth && !userLogged) {
        return {
            name: "Login",
        }
    }

    if (to.meta.guestOnly && userLogged) {
        return {
            name: "Home"
        }
    }
}));

export default router
