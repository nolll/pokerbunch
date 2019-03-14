import moment from 'moment';

export default {
    diffInMinutes: function(a, b) {
        const diff = moment.duration(b.diff(a));
        return Math.round(Math.abs(diff.asMinutes()));
    }
};
