import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getMenus = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/menus`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const createMenu = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/menus`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.post(api, data, { headers: headers });

        if (res.status === 200) {
            return res.data;
        }
    } catch (error) {
        console.log(error);

        if (error.response.status === 400) {
            return error.response.data;
        }
    }
}

const updateMenu = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/menus`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.put(api, data, { headers: headers });

        if (res.status === 204) {
            return true;
        }
    } catch (error) {
        console.log(error);

        if (error.response.status === 400) {
            return false;
        }
    }
}

const getMenu = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/menus/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const getProductInMenu = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const id = params.id;
    const query = params.queryString;

    const api = `${HOST_API}/${API_VERSION}/menus/${id}/products`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };


    try {
        const res = await axios.get(api, { headers: headers, params: query });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const addProductInMenu = async (spData) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const id = spData.menuId;
    const data = spData.data;

    const api = `${HOST_API}/${API_VERSION}/menus/${id}/products`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.post(api, data, { headers: headers });

        if (res.status === 200) {
            return true;
        }
    } catch (error) { console.log(error) }
}

const updateProductInMenu = async (spData) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const id = spData.menuId;
    const data = spData.data;

    const api = `${HOST_API}/${API_VERSION}/menus/${id}/products`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.put(api, data, { headers: headers });

        if (res.status === 204) {
            return { isSuccess: true };
        }
    } catch (error) {
        console.log(error);

        if (error.response.status === 400) {
            return error.response.data;
        }
    }
}

export {
    getMenus, createMenu, updateMenu, getMenu, getProductInMenu, addProductInMenu, updateProductInMenu
}