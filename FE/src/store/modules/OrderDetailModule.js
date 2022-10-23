import { getOrderDetail } from "../../services/OrderDetailSerivce";

export default {
    namespaced: true,
    state: {
        orderDetail: [],
    },
    mutations: {
        setOrderDetail(state, data) {
            state.orderDetail = data;
        }
    },
    actions: {
        async getOrderDetail({ commit }, orderId) {
            const data = await getOrderDetail(orderId);
            commit('setOrderDetail', data);
        }
    },
}