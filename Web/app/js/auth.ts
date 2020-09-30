import storage from './storage';

const tokenKey = 'token';

export default {
    getToken() {
        return storage.get(tokenKey);
    },
    setToken(token: string, persist: boolean) {
        storage.set(tokenKey, token, persist);
    },
    clearToken() {
        storage.delete(tokenKey);
    },
    isLoggedIn: function() {
        return !!this.getToken();
    }
};
