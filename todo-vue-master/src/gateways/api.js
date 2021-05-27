import axios from "axios";
import store from "@/store";

const api = axios.create({
    baseURL: 'https://localhost:5001/api/',

});

api.interceptors.request.use(function (config) {
    if (store.getters["auth/isLogged"]) {
        const token = store.state.auth.jwtToken;
        config.headers.Authorization = `Bearer ${token}`;
    }

    return config;
});
export default api;
