function select(field:ITextInput, start:number, end:number) {
    if (field.createTextRange) {
        const selRange = field.createTextRange();
        selRange.collapse(true);
        selRange.moveStart('character', start);
        selRange.moveEnd('character', end);
        selRange.select();
        field.focus();
    } else if (field.setSelectionRange) {
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
    selectAll(field:ITextInput) {
        return select(field, 0, field.value.length);
    }
};
