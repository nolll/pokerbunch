function hasValue(val: number | null | undefined) {
  return val !== undefined && val !== null;
}

export default {
  intRange(val: number, min: number | null | undefined, max?: number | null | undefined) {
    if (hasValue(min) && val < min!) return false;
    if (hasValue(max) && val > max!) return false;
    return true;
  },
};
