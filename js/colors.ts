const colors = ['#003f5c', '#2f4b7c', '#665191', '#a05195', '#d45087', '#f95d6a', '#ff7c43', '#ffa600', '#aaaaaa'];

export const getColor = (index: number) => (index < colors.length ? colors[index] : colors[colors.length - 1]);
