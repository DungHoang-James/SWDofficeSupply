import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getCompanies = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/companies`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const getCompany = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/companies/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const createCompany = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/companies`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.post(api, data, { headers: headers });

        return res.data;
    } catch (error) {
        console.log(error);
    }
}

const updateCompany = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/companies`;

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

export {
    getCompanies, getCompany, createCompany, updateCompany
}