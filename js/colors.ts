const colors = [
  '#E6194B',
  '#3CB44B',
  '#FFE119',
  '#4363D8',
  '#F58231',
  '#911EB4',
  '#42D4F4',
  '#F032E6',
  '#BFEF45',
  '#FABEBE',
  '#469990',
  '#E6BEFF',
  '#9A6324',
  '#FFFAC8',
  '#800000',
  '#AAFFC3',
];

export const getColor = (index: number) => (index < colors.length ? colors[index] : colors[colors.length - 1]);
