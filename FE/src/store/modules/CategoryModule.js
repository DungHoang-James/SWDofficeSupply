import { createCategory, getCategories, updateCategory, deleteCategory, getCategory, getCategoriesMin } from '../../services/CategoryService'
// import { getCategoryStatistics } from "../../services/StatisticsService"

export default {
    namespaced: true,
    state: {
        categories: {
            currentPage: 0,
            totalPage: 0,
            pageSize: 0,
            totalCount: 0,
            items: [],
            hasPrevious: false,
            hasNext: false
        },
        category: null,
        categoriesMin: null,
    },
    mutations: {
        setCategories(state, data) {
            state.categories = data;
        },

        setCategory(state, data) {
            state.category = data;
        },

        setCategoriesMin(state, data) {
            state.categoriesMin = data;
        }
    },
    actions: {
        async getCategoryAction({ commit }, params) {
            // await getCategoryStatistics();
            const data = await getCategories(params);
            commit('setCategories', data);
        },

        async getCategory({ commit }, id) {
            const data = await getCategory(id);
            commit('setCategory', data);
        },

        async getCategoriesMin({ commit }, params) {
            const data = await getCategoriesMin(params);
            commit('setCategoriesMin', data);
        },

        async createCategory({ dispatch }, data) {
            const res = await createCategory(data);
            if (res.isSuccess) {
                dispatch("getCategoryAction");
            }
        },

        async updateCategory({ dispatch }, data) {
            const res = await updateCategory(data);
            if (res) {
                dispatch("getCategoryAction");
            }
        },

        async deleteCategory({ dispatch }, id) {
            const res = await deleteCategory(id);
            if (res) {
                dispatch("getCategoryAction");
            }
        }
    },
}