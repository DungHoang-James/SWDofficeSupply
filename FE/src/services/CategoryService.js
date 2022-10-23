import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

const getCategoriesMin = async (params) => {
    const api = `${HOST_API}/${API_VERSION}/catergories`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
        return error.data;
    }
}

const getCategories = async (params) => {
    const api = `${HOST_API}/${API_VERSION}/catergories`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
        return error.data;
    }
}

const getCategory = async (id) => {
    const api = `${HOST_API}/${API_VERSION}/catergories/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
        return error.data;
    }
}

const createCategory = async (data) => {
    const api = `${HOST_API}/${API_VERSION}/catergories`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.post(api, data, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
        return error.data;
    }
}

const updateCategory = async (data) => {
    const api = `${HOST_API}/${API_VERSION}/catergories`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.put(api, data, { headers: headers });
        if (res.status === 204) {
            return true;
        }
    } catch (error) {
        console.log(error);
        return error.data;
    }
}

const deleteCategory = async (id) => {
    const api = `${HOST_API}/${API_VERSION}/catergories/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.delete(api, { headers: headers });
        if (res.status === 204) {
            return true;
        }
    } catch (error) {
        console.log(error);
        return error.data;
    }
}

export {
    getCategories, createCategory, updateCategory, deleteCategory, getCategory, getCategoriesMin
}