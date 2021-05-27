import _ from "lodash";
import api from "@/gateways/api";

function parseTokenPayload(token) {
    return JSON.parse(atob(token.split(".")[1]));
}

function getToken() {
    const token = localStorage.getItem("jwtToken");
    if (token === null) {
        return null;
    }

    const payload = parseTokenPayload(token);

    const unixEpochStart = new Date(0);
    const tokenExpirationDate = unixEpochStart.setUTCSeconds(payload.exp);
    const currentDate = new Date();

    if (currentDate >= tokenExpirationDate) {
        localStorage.removeItem("jwtToken");
        return null;
    }

    return token;
}

const state = {
    jwtToken: getToken(),
    errors: null
}

const getters = {
    isLogged: state => !!state.jwtToken,
    errors: state => state.errors,
    user: state => parseTokenPayload(state.jwtToken)
}

const actions = {
    async login({commit}, loginModel) {
        try {
            const response = await api.post("auth/login", loginModel);
            commit("setErrors", null);
            commit("loginUser", response.data.token);
        } catch (ex) {
            const data = ex.response.data;

            if (_.has(data, "errors")) {
                commit("setErrors", data.errors);
            }
        }
    },
    logout({commit}) {
        commit("logoutUser");
    },
    async register({commit}, registerModel) {
        try {
            await api.post("auth/register", registerModel);
            commit("setErrors", null);
        } catch (ex) {
            const data = ex.response.data;

            if (_.has(data, "errors")) {
                commit("setErrors", data.errors);
            }
        }
    }
}

const mutations = {
    setErrors(state, errors) {
        state.errors = errors;
    },
    loginUser(state, token) {
        localStorage.setItem("jwtToken", token);

        state.jwtToken = token;
        state.authError = "";
    },
    logoutUser(state) {
        localStorage.removeItem("jwtToken");

        state.jwtToken = null;
        state.authError = "";
    }
}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};