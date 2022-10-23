import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getDepartments = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/departments`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const createDepartment = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/departments`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.post(api, data, { headers: headers });

        if (res.status === 200) {
            return true;
        }
    } catch (error) {
        console.log(error);
        return false;
    }
}

const getDepartment = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/departments/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const updateDepartment = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/departments`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.put(api, data, { headers: headers });

        if (res.status === 204) {
            return true;
        }
    } catch (error) {
        console.log(error);
        return false;
    }
}

const getUserInDepartment = async (resource) => {
    const id = resource.id;
    const params = resource.params;

    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/departments/${id}/users`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

export {
    getDepartments, createDepartment, getDepartment, updateDepartment, getUserInDepartment
}