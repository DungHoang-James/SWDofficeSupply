import axios from 'axios';
import { HOST_API, API_VERSION } from '../data/constants/AppConstant'

const getOrderDetail = async (orderId) => {
    const jwtToken = JSON.parse(sessionStorage.getItem('jwtToken'));

    const api = `${HOST_API}/${API_VERSION}/orderdetails/${orderId}`;

    const headers = { 'Authorization': `bearer ${jwtToken.jwtToken}` };

    try {
        const res = await axios.get(api, { headers: headers });

        return res.data.responseData;
    } catch (error) {
        console.log(error);
    }
}

export {
    getOrderDetail,
}