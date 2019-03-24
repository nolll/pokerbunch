import cookies from './cookies'

const cookieName = 'token';

export default {
    getToken() {
        return cookies.get(cookieName);
    },
    setToken(token: string) {
        cookies.set(cookieName, token, 365);
    },
    isLoggedIn: function() {
        return !!this.getToken();
    }
};
