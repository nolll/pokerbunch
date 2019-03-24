function hasValue(val:number) {
    return val !== undefined && val !== null;
}

export default {
    intRange(val:number, min:number, max:number) {
        if (hasValue(min) && val < min)
            return false;
        if (hasValue(max) && val > max)
            return false;
        return true;
    }
}