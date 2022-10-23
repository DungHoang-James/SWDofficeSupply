import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getCurrentPeriodOfDepartment = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/periods/department/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
        if (error.response.status === 404) {
            return {
                id: 0,
                name: '',
                departmentID: 0,
                fromTime: 0,
                toTime: 0,
                quota: 0,
                remainingQuota: 0,
                isExpired: false,
            };
        }
    }
}

const getPeriods = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/periods`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const createPeriod = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/periods`;

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

const updatePeriod = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/periods`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.put(api, data, { headers: headers });

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

export {
    getCurrentPeriodOfDepartment, getPeriods, createPeriod, updatePeriod
}