const colors = [
  '#9bccd5',
  '#ffd3bf',
  '#6290bf',
  '#c32750',
  '#e8fce1',
  '#d84460',
  '#416ab0',
  '#ffaaa2',
  '#b6e2dc',
  '#93003a',
  '#80b0cc',
  '#ea6372',
  '#cff2e0',
  '#f98588',
  '#00429d',
  '#ac0d44',
];

export const getColor = (index: number) => (index < colors.length ? colors[index] : colors[colors.length - 1]);
