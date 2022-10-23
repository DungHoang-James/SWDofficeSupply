import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getOrders = async (params) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/orders`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers, params: params });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

const updateOrder = async (data) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));
    const api = `${HOST_API}/${API_VERSION}/orders`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };
console.log(data);
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

export {
    getOrders, updateOrder
}