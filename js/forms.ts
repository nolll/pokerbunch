function select(field: HTMLInputElement, start: number, end: number) {
  field.setSelectionRange(start, end);
}

export default {
  selectAll(field: HTMLInputElement) {
    return select(field, 0, field.value.length);
  },
  parseInt(str: string): number {
    const parsed = parseInt(str);
    if (isNaN(parsed)) {
      return 0;
    }
    return parsed;
  },
};
