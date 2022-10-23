import { getArea, getAreas, getAreasMin } from "../../services/AreaService"

export default {
    namespaced: true,
    state: {
        areas: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        areasMin: null,
        area: null,
    },
    mutations: {
        setAreas(state, data) {
            state.areas = data;
        },
        setAreasMin(state, data) {
            state.areasMin = data;
        },
        setArea(state, data) {
            state.area = data;
        }
    },
    actions: {
        async getAreas({ commit }, params) {
            const data = await getAreas(params);
            commit('setAreas', data);
        },
        async getAreasMin({ commit }, params) {
            const data = await getAreasMin(params);
            commit('setAreasMin', data);
        },
        async getArea({ commit }, id) {
            const data = await getArea(id);
            commit('setArea', data);
        }
    },
}