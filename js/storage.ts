export default {
    get(key: string) {
        const persistent = localStorage.getItem(key);
        if (persistent)
            return persistent;

        return sessionStorage.getItem(key);
    },
    set(key: string, value: string, persist: boolean) {
        if (persist)
            localStorage.setItem(key, value);
        else
            sessionStorage.setItem(key, value);
    },
    delete(key: string) {
        localStorage.removeItem(key);
        sessionStorage.removeItem(key);
    }
};
