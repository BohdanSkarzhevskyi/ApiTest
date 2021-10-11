import axios from 'axios'
import { toast } from 'react-semantic-toasts';
// create an axios instance
const service = axios.create({
  timeout: 5000 // request timeout
})

// response interceptor
service.interceptors.response.use(
    response => {
        let res = response.data
        if (response.headers['content-type'] === 'application/octet-stream') {
            res = response
        }
            
        if (!(response.status === 200 || response.status === 201)) {
            let errorMessage = 'Error'
            switch (res.status) {
                case 400:
                    errorMessage = 'Invalid request'
                    break;
                case 401:
                    errorMessage = 'Not allowed'
                    break;
                case 404:
                    errorMessage = 'Not found'
                    break;
                case 500:
                    errorMessage = 'Unexpected error'
                    break;
                default:
            }
            toast({
                type: 'warning',
                icon: 'envelope',
                title: 'Error',
                description: res.message || errorMessage,
                time: 5000
            });
            return Promise.reject(new Error(res.message || 'Error'));
        } else {
            return res;
        }
    },
    error => {
        console.log('err' + (error?.response?.data || error)); // for debug
        toast({
            type: 'error',
            title: 'Error',
            description:  error?.response?.data?.Message || error?.response?.data || error?.message || error,
            time: 5000
        });
        return Promise.reject(error);
    }
)
export default service;