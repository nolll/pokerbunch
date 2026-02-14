export default {
  get: (key: string) => {
    const persistent = localStorage.getItem(key);
    return persistent ? persistent : sessionStorage.getItem(key);
  },
  set: (key: string, value: string, persist: boolean) => {
    if (persist) localStorage.setItem(key, value);
    else sessionStorage.setItem(key, value);
  },
  delete: (key: string) => {
    localStorage.removeItem(key);
    sessionStorage.removeItem(key);
  },
};
