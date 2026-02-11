import dayjs from 'dayjs';

export const diffInMinutes = (a: Date, b: Date) => Math.abs(dayjs(a).diff(dayjs(b), 'minute'));
