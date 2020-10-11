function select(field: HTMLInputElement, start: number, end: number) {
    field.setSelectionRange(start, end);
}

export default {
    selectAll(field: HTMLInputElement) {
        return select(field, 0, field.value.length);
    }
};
