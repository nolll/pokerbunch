export default {
    compare(a: any, b: any) {
        if (a < b)
            return -1;
        if (a > b)
            return 1;
        return 0;
    }
};
