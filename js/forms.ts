const select = (field: HTMLInputElement, start: number, end: number) => {
  field.setSelectionRange(start, end);
};

export default {
  selectAll: (field: HTMLInputElement) => select(field, 0, field.value.length),
  parseInt: (str: string): number => {
    const parsed = parseInt(str);
    return isNaN(parsed) ? 0 : parsed;
  },
};
