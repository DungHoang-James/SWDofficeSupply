import { getUser, getUserOfCompany, getUsers } from "../../services/UserService";

export default {
    namespaced: true,
    state: {
        user: null,
        usersOfCompany: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        usersMin: null,
        users: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        userPicked: null,
    },
    mutations: {
        setUser(state, data) {
            state.user = data;
        },
        setUsersOfCompany(state, data) {
            state.usersOfCompany = data;
        },
        setUsersMin(state, data) {
            state.usersMin = data;
        },
        setUsers(state, data) {
            state.users = data;
        },
        setUserPicked(state, data) {
            state.userPicked = data;
        }
    },
    actions: {
        async getUser({ commit }, id) {
            const data = await getUser(id);
            sessionStorage.setItem('userInfo', JSON.stringify(data));
            commit('setUser', data);
        },

        async getUserPicked({ commit }, id) {
            const data = await getUser(id);
            commit('setUserPicked', data);
        },

        async getUsersOfCompany({ commit }, params) {
            const data = await getUserOfCompany(params);
            commit('setUsersOfCompany', data);
        },

        async getUsersMin({ commit }, params) {
            const data = await getUsers(params);
            commit('setUsersMin', data);
        },

        async getUsers({ commit }, params) {
            const data = await getUsers(params);
            commit('setUsers', data);
        }
    },
}