import dayjs from 'dayjs';
import { Localization } from './models/Localization';

function formatCurrency(value: number, localization: Localization) {
  const f = localization.currencyFormat !== undefined ? localization.currencyFormat : '${0}';
  const s = localization.thousandSeparator !== undefined ? localization.thousandSeparator : ',';
  const v = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, s);
  return f.replace('{0}', v);
}

function formatResult(value: number, localization: Localization) {
  const absValue = Math.abs(value);
  const currencyValue = formatCurrency(absValue, localization);
  if (value > 0) return `+${currencyValue}`;
  if (value < 0) return `-${currencyValue}`;
  return currencyValue;
}

function formatResultWithoutCurrency(value: number) {
  const absValue = Math.abs(value);
  if (value > 0) return `+${absValue}`;
  if (value < 0) return `-${absValue}`;
  return absValue.toString();
}

function formatWinrate(value: number, localization: Localization) {
  const currencyValue = formatCurrency(value, localization);
  return currencyValue + '/h';
}

function formatDuration(minutes: number) {
  const h = Math.floor(minutes / 60);
  const m = minutes % 60;
  if (h > 0 && m > 0) return h + 'h ' + m + 'm';
  if (h > 0) return h + 'h';
  return m + 'm';
}

function formatMonthDay(date: Date) {
  return dayjs(date).format('MMM D');
}

function formatHourMinute(date: Date) {
  return dayjs(date).format('HH:mm');
}

function formatMonthDayYear(date: Date) {
  return dayjs(date).format('MMM D YYYY');
}

function formatIsoTime(date: Date) {
  return dayjs(date).toISOString();
}

function formatLocalTime(date: Date) {
  return dayjs(date).format('YYYY-MM-DD HH:mm:ss');
}

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
