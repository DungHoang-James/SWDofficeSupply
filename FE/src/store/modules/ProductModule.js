import { getProduct, getProducts } from "../../services/ProductService";

export default {
    namespaced: true,
    state: {
        products: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        product: null,
    },
    mutations: {
        setProducts(state, data) {
            state.products = data;
        },
        setProduct(state, data) {
            state.product = data;
        }
    },
    actions: {
        async getProducts({ commit }, params) {
            const data = await getProducts(params);
            commit('setProducts', data);
        },
        async getProduct({ commit }, id) {
            const data = await getProduct(id);
            commit('setProduct', data);
        }
    },
}