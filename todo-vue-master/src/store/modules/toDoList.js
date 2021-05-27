import _ from "lodash";
import api from "@/gateways/api";
import toHumanFriendlyDate from "@/composables/toHumanFriendlyDate";

const state = {
    list: null,
    updating: false
}

const getters = {
    listDateCreated: state => toHumanFriendlyDate(state.list.dateCreated),
    listDateChanged: state => toHumanFriendlyDate(state.list.dateChanged),
    canAddItem: state => _.find(state.list.toDoItems, i => i.id === 0) === undefined
}

const actions = {
    async fetchList({commit}, id) {
        const {data} = await api.get(`todolists/${id}`)
        commit("setList", data);
    },
    async updateList({commit, state}, newName) {
        commit("setUpdating", true);

        const {data} = await api.put(`todolists`, {id: state.list.id, name: newName});

        commit("setUpdating", false);
        commit("updateList", data);
    },
    addToDoItem({commit}) {
        commit("addToDoItem");
    },
    async updateToDoItem({state, commit}, updatedItem) {
        const itemIndex = _.findIndex(state.list.toDoItems, i => i.id === updatedItem.id);

        commit("setUpdating", true);
        if (updatedItem.id === 0) {
            const {data} = await api.post(`todoitems`, {...updatedItem});
            commit("updateToDoItem", {itemIndex, data});
        } else {
            const {data} = await api.put(`todoitems`, {...updatedItem});
            commit("updateToDoItem", {itemIndex, data});
        }
        commit("setUpdating", false);
    },
    async deleteToDoItem({state, commit}, id) {
        const itemIndex = _.findIndex(state.list.toDoItems, i => i.id === id);

        commit("setUpdating", true);
        if (id === 0) {
            commit("deleteToDoItem", itemIndex);
        } else {
            await api.delete(`todoitems/${id}`);
            commit("deleteToDoItem", itemIndex);
        }
        commit("setUpdating", false);
    }
}

const mutations = {
    setUpdating(state, value) {
        state.updating = value;
    },
    setList(state, list) {
        state.list = list;
    },
    updateList(state, updatedList) {
        state.list.name = updatedList.name;
        state.list.dateChanged = updatedList.dateChanged;
    },
    addToDoItem(state) {
        state.list.toDoItems.push({id: 0, text: "", isCompleted: false, toDoListId: state.list.id})
    },
    updateToDoItem(state, payload) {
        state.list.toDoItems.splice(payload.itemIndex, 1, payload.data);
    },
    deleteToDoItem(state, itemIndex) {
        state.list.toDoItems.splice(itemIndex, 1);
    }
}


export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};