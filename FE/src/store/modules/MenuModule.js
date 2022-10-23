import { getMenu, getMenus, getProductInMenu } from "../../services/MenuService";

export default {
    namespaced: true,
    state: {
        menus: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        menusMin: null,
        menu: null,
        productInMenu: {
            currentPage: null,
            totalPage: null,
            pageSize: null,
            totalCount: null,
            items: [],
            hasPrevious: null,
            hasNext: null,
        },
        productInMenuMin: null,
    },
    mutations: {
        setMenus(state, data) {
            state.menus = data;
        },
        setMenusMin(state, data) {
            state.menusMin = data;
        },
        setMenu(state, data) {
            state.menu = data;
        },
        setProductInMenu(state, data) {
            state.productInMenu = data;
        },
        setProductInMenuMin(state, data) {
            state.productInMenuMin = data;
        },
    },
    actions: {
        async getMenus({ commit }, params) {
            const data = await getMenus(params);
            commit('setMenus', data);
        },
        async getMenusMin({ commit }, params) {
            const data = await getMenus(params);
            commit('setMenusMin', data);
        },
        async getMenu({ commit }, id) {
            const data = await getMenu(id);
            commit('setMenu', data);
        },

        async getProductInMenu({ commit }, params) {
            const data = await getProductInMenu(params);
            console.log(data);
            commit('setProductInMenu', data);
        },
        async getProductInMenuMin({ commit }, params) {
            const data = await getProductInMenu(params);
            console.log(data);
            commit('setProductInMenuMin', data);
        }
    },
}