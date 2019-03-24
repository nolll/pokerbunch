export default {
    get(name: string) {
        const re = new RegExp(name + '=([^;]+)');
        const value = re.exec(document.cookie);
        return (value) ? unescape(value[1]) : null;
    },
    set(name: string, value: string, days: number) {
        let expires = '';
        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            const utcString = date.toUTCString();
            expires = `; expires=${utcString}`;
        }
        document.cookie = name + '=' + (value || '') + expires + '; path=/';
    },
    delete(name:string) {
        document.cookie = name + '=; Max-Age=-99999999;';
    }
};
