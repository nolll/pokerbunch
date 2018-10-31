import cookies from './cookies'

var cookieName = 'token';

export default {
    getToken: function() {
        return cookies.get(cookieName);
    },
    setToken: function(token) {
        cookies.set(cookieName, token, 365);
    },
    isLoggedIn: function() {
        return !!this.getToken();
    }
};
