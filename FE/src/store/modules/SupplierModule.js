import { getSupplier, getSuppliers, getSuppliersMin } from "../../services/SupplierService"

export default {
    namespaced: true,
    state: {
        suppliers: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        suppliersMin: null,
        supplier: null,
    },
    mutations: {
        setSuppliers(state, data) {
            state.suppliers = data;
        },

        setSuppliersMin(state, data) {
            state.suppliersMin = data;
        },

        setSupplier(state, data) {
            state.supplier = data;
        }
    },
    actions: {
        async getSuppliers({ commit }, params) {
            const data = await getSuppliers(params);
            commit('setSuppliers', data);
        },

        async getSuppliersMin({ commit }, params) {
            const data = await getSuppliersMin(params);
            commit('setSuppliersMin', data);
        },

        async getSupplier({ commit }, id) {
            const data = await getSupplier(id);
            commit('setSupplier', data);
        }
    },
}