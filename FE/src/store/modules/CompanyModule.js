import { getCompanies, getCompany } from "../../services/CompanyService"

export default {
    namespaced: true,
    state: {
        companies: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        company: {
            address: null,
            areaID: null,
            email: null,
            id: null,
            isDelete: null,
            managerID: null,
            name: null,
            phoneNumber: null,
            wallet: null
        },
    },
    mutations: {
        setCompanies(state, data) {
            state.companies = data;
        },

        setCompany(state, data) {
            state.company = data;
        }
    },
    actions: {
        async getCompanies({ commit }, params) {
            const data = await getCompanies(params);
            commit('setCompanies', data);
        },

        async getCompany({ commit }, id) {
            const data = await getCompany(id);
            commit('setCompany', data);
        }
    },
}