import { getCurrentPeriodOfDepartment, getPeriods } from "../../services/PeriodService";

export default {
    namespaced: true,
    state: {
        currentPeriodOfDepartment: {
            id: null,
            name: null,
            departmentID: null,
            fromTime: null,
            toTime: null,
            quota: null,
            remainingQuota: null,
            isExpired: null,
        },
        periods: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
    },
    mutations: {
        setCurrentPeriodOfDepartment(state, data) {
            state.currentPeriodOfDepartment = data;
        },
        setPeriods(state, data) {
            state.periods = data;
        },
    },
    actions: {
        async getCurrentPeriodOfDepartment({ commit }, id) {
            const data = await getCurrentPeriodOfDepartment(id);
            commit('setCurrentPeriodOfDepartment', data);
        },

        async getPeriods({ commit }, params) {
            const data = await getPeriods(params);
            commit('setPeriods', data);
        }
    },
}