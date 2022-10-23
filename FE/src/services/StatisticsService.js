//statistics/categories
import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getCategoryStatistics = async () => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/statistics/categories`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data;
    } catch (error) {
        console.log(error);
    }
}

const getDepartmentStatistics = async () => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/statistics/departments`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data;
    } catch (error) {
        console.log(error);
    }
}

const getAreaStatistics = async () => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/statistics/areas`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data;
    } catch (error) {
        console.log(error);
    }
}

export {
    getCategoryStatistics, getDepartmentStatistics, getAreaStatistics
}