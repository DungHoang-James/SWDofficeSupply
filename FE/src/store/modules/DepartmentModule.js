import { getDepartment, getDepartments, getUserInDepartment } from "../../services/DepartmentService";

export default {
    namespaced: true,
    state: {
        departments: [],
        department: {
            id: null,
            name: null,
            phoneNumber: null,
            email: null,
            companyID: null,
            isDelete: null
        },
        userInDepartment: {
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
        setDepartments(state, data) {
            state.departments = data;
        },
        setDepartment(state, data) {
            state.department = data;
        },
        setUserInDepartment(state, data) {
            state.userInDepartment = data;
        },
    },
    actions: {
        async getDepartments({ commit }, params) {
            const data = await getDepartments(params);
            commit('setDepartments', data);
        },

        async getDepartment({ commit }, id) {
            const data = await getDepartment(id);
            commit('setDepartment', data);
        },

        async getUserInDepartment({ commit }, resource) {
            const data = await getUserInDepartment(resource);
            commit('setUserInDepartment', data);
        }
    },
}