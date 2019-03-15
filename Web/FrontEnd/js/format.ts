export default {
    currency: formatCurrency,
    result: formatResult,
    winrate: formatWinrate,
    time: formatTime
};

function formatCurrency(value: number, format: string, separator: string) {
    const f = format !== undefined ? format : '${0}';
    const s = separator !== undefined ? separator : ',';
    const v = value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, s);
    return f.replace('{0}', v);
}

function formatResult(value: number, format: string, separator:string) {
    const absValue = Math.abs(value);
    const currencyValue = formatCurrency(absValue, format, separator);
    if (value > 0)
        return `+${currencyValue}`;
    if (value < 0)
        return `-${currencyValue}`;
    return currencyValue;
}

function formatWinrate(value: number, format: string, separator: string) {
    const currencyValue = formatCurrency(value, format, separator);
    return currencyValue + '/h';
}

function formatTime(minutes: number) {
    const h = Math.floor(minutes / 60);
    const m = minutes % 60;
    if (h > 0 && m > 0)
        return h + 'h ' + m + 'm';
    if (h > 0)
        return h + 'h';
    return m + 'm';
}
