import request from '../utils/request'

const dataService = {

    async getStudentStats() {
        return request({
            url: '/api/students/stats',
            method: 'get'
        });
    },

    async studentsAggregate(data) {
        return request({
            url: '/api/students/aggregate',
            method: 'post',
            data
        });
    },
}

export default dataService;