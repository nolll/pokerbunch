import storage from './storage';

const tokenKey = 'token';
const refreshTokenKey = 'refreshToken';

const getToken = () => storage.get(tokenKey);
const getRefreshToken = () => storage.get(refreshTokenKey);

const setToken = (token: string, persist: boolean) => {
  storage.set(tokenKey, token, persist);
};

const setRefreshToken = (token: string, persist: boolean) => {
  storage.set(refreshTokenKey, token, persist);
};

const logout = () => {
  clearToken();
  clearRefreshToken();
};

const clearToken = () => {
  storage.delete(tokenKey);
};

const clearRefreshToken = () => {
  storage.delete(refreshTokenKey);
};

const hasToken = () => Boolean(getToken());
const hasRefreshToken = () => Boolean(getToken());

export default {
  getToken,
  getRefreshToken,
  setToken,
  setRefreshToken,
  hasToken,
  hasRefreshToken,
  logout,
};
