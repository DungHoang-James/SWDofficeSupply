import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getAreas = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/areas`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const getAreasMin = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/areas`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const createArea = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/areas`;

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

const getArea = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/areas/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);

        if (error.response.status === 400) {
            return error.response.data;
        }
    }
}

const updateArea = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/areas`;

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
    getAreas, getAreasMin, createArea, getArea, updateArea
}