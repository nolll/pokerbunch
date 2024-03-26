import storage from './storage';

const tokenKey = 'token';

const getToken = () => storage.get(tokenKey);

const setToken = (token: string, persist: boolean) => {
  storage.set(tokenKey, token, persist);
};

const clearToken = () => {
  storage.delete(tokenKey);
};

const hasToken = () => !!getToken();

export default {
  getToken,
  setToken,
  clearToken,
  hasToken,
};
