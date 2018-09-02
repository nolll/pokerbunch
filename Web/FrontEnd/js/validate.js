function intRange(val, min, max) {
    if (hasValue(min) && val < min)
        return false;
    if (hasValue(max) && val > max)
        return false;
    return true;
}

function hasValue(val) {
    return val !== undefined && val !== null;
}

export default {
    intRange: intRange
}