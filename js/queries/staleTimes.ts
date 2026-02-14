const minutes = (m: number) => seconds(60 * m);
const seconds = (s: number) => s * 1000;

export const thirtySecondsStaleTime = seconds(30);
export const fiveMinuteStaleTime = minutes(5);
export const fifteenSecondsRefreshInterval = seconds(15);
export const thirtySecondsRefreshInterval = seconds(30);
