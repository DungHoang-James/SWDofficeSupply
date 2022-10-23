import { getOrders } from "../../services/OrderService";

export default {
    namespaced: true,
    state: {
        orders: {
            currentPage: null,
            hasNext: null,
            hasPrevious: null,
            items: [],
            pageSize: null,
            totalCount: null,
            totalPage: null,
        },
    },
    mutations: {
        setOrders(state, data) {
            state.orders = data;
        }
    },
    actions: {
        async getOrders({ commit }, params) {
            console.log(params);
            const data = await getOrders(params);
            console.log(data);
            commit('setOrders', data);
        }
    },
}