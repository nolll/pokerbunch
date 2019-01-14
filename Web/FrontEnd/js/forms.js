function select(field, start, end) {
    if (field.createTextRange) {
        const selRange = field.createTextRange();
        selRange.collapse(true);
        selRange.moveStart('character', start);
        selRange.moveEnd('character', end);
        selRange.select();
        field.focus();
    } else if (field.setSelectionRange) {
        window.setTimeout(function () {
            field.focus();
            field.setSelectionRange(start, end);
        }, 1);
    } else if (typeof field.selectionStart !== 'undefined') {
        field.selectionStart = start;
        field.selectionEnd = end;
        field.focus();
    }
}

function selectAll(field) {
    return select(field, 0, field.value.length);
}

export default {
    selectAll: selectAll
};
