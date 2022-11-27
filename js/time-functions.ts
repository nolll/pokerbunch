import dayjs from 'dayjs';

export default {
    diffInMinutes(a: Date, b: Date) {
        const diffMinutes = dayjs(a).diff(dayjs(b), 'minute');
        return Math.abs(diffMinutes);
    }
};
