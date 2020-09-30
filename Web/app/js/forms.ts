function select(field: HTMLInputElement, start:number, end:number) {
    if (field.setSelectionRange) {
        window.setTimeout(() => {
            field.focus();
            field.setSelectionRange(start, end);
        }, 1);
    } else if (typeof field.selectionStart !== 'undefined') {
        field.selectionStart = start;
        field.selectionEnd = end;
        field.focus();
    }
}

export default {
    selectAll(field: HTMLInputElement) {
        return select(field, 0, field.value.length);
    }
};
