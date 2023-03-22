import storage from './storage';
import urls from './urls';

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
  isLoggedIn() {
    return !!this.getToken();
  },
  requireUser() {
    if (!this.isLoggedIn()) window.location.href = urls.home;
  },
};
