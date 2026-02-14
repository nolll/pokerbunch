import dayjs from 'dayjs';
import { Localization } from './models/Localization';

const formatCurrency = (value: number, localization: Localization) => {
  const f = localization.currencyFormat !== undefined ? localization.currencyFormat : '${0}';
  const s = localization.thousandSeparator !== undefined ? localization.thousandSeparator : ',';
  const v = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, s);
  return f.replace('{0}', v);
}

const formatResult = (value: number, localization: Localization) => {
  const absValue = Math.abs(value);
  const currencyValue = formatCurrency(absValue, localization);
  if (value > 0) return `+${currencyValue}`;
  if (value < 0) return `-${currencyValue}`;
  return currencyValue;
}

const formatResultWithoutCurrency = (value: number) => {
  const absValue = Math.abs(value);
  if (value > 0) return `+${absValue}`;
  if (value < 0) return `-${absValue}`;
  return absValue.toString();
}

const formatWinrate = (value: number, localization: Localization) => {
  const currencyValue = formatCurrency(value, localization);
  return currencyValue + '/h';
}

const formatDuration = (minutes: number) => {
  const h = Math.floor(minutes / 60);
  const m = minutes % 60;
  if (h > 0 && m > 0) return h + 'h ' + m + 'm';
  if (h > 0) return h + 'h';
  return m + 'm';
}

const formatMonthDay = (date: Date) => dayjs(date).format('MMM D')
const formatHourMinute = (date: Date) => dayjs(date).format('HH:mm')
const formatMonthDayYear = (date: Date) => dayjs(date).format('MMM D YYYY')
const formatIsoTime = (date: Date) => dayjs(date).toISOString()
const formatLocalTime = (date: Date) => dayjs(date).format('YYYY-MM-DD HH:mm:ss')

export default {
  currency: formatCurrency,
  result: formatResult,
  resultWithoutCurrency: formatResultWithoutCurrency,
  winrate: formatWinrate,
  duration: formatDuration,
  monthDay: formatMonthDay,
  hourMinute: formatHourMinute,
  monthDayYear: formatMonthDayYear,
  isoTime: formatIsoTime,
  localTime: formatLocalTime,
};
