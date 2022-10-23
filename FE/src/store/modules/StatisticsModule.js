import { getAreaStatistics, getCategoryStatistics, getDepartmentStatistics } from "../../services/StatisticsService";

export default {
    namespaced: true,
    state: {
        categoryStatistics: [],
        departmentStatistics: [],
        areaStatistics: [],
    },
    mutations: {
        setCategoryStatistics(state, data) {
            state.categoryStatistics = data;
        },
        setDepartmentStatistics(state, data) {
            state.departmentStatistics = data;
        },
        setAreaStatistics(state, data) {
            state.areaStatistics = data;
        }
    },
    actions: {
        async getCategoryStatistics({ commit }) {
            const data = await getCategoryStatistics();
            commit('setCategoryStatistics', data);
        },
        async getDepartmentStatistics({ commit }) {
            const data = await getDepartmentStatistics();
            commit('setDepartmentStatistics', data);
        },
        async getAreaStatistics({ commit }) {
            const data = await getAreaStatistics();
            commit('setAreaStatistics', data);
        }
    },
}