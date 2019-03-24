import * as moment from 'moment';

export default {
    diffInMinutes(a: moment.Moment, b: moment.Moment) {
        const diff = moment.duration(b.diff(a));
        return Math.round(Math.abs(diff.asMinutes()));
    }
};
