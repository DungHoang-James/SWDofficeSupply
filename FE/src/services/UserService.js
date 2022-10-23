import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getToken = async (accessToken) => {
    const data = { idToken: accessToken };
    const api = `${HOST_API}/${API_VERSION}/auth/token`;

    try {
        const response = await axios.post(api, data);

        return response.data;
    } catch (error) {
        if (error.response.status === 400) {
            console.log(error.response.data);
        }
    }
}

const getUser = async (id) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/users/${id}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const addUserToDepartment = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/users`;

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

const getUserOfCompany = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/companies/${params.id}/users`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params.params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const getUsers = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/users`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

export {
    getToken, getUser, addUserToDepartment, getUserOfCompany, getUsers
}