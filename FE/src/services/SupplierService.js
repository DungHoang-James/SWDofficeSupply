import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getSuppliers = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/suppliers`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const getSuppliersMin = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/suppliers`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const createSupplier = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/suppliers`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.post(api, data, { headers: headers });

        return res.data;
    } catch (error) {
        console.log(error);
    }
}

const getSupplier = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/suppliers/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const updateSupplier = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/suppliers`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.put(api, data, { headers: headers });

        if (res.status === 204) {
            return true;
        }
    } catch (error) {
        console.log(error);
    }
}

export { getSuppliers, getSuppliersMin, createSupplier, getSupplier, updateSupplier }