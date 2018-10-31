export default {
    get: function (name) {
        const re = new RegExp(name + '=([^;]+)');
        const value = re.exec(document.cookie);
        return (value != null) ? unescape(value[1]) : null;
    },
    set: function(name, value, days) {
        var expires = '';
        if (days) {
            const date = new Date();
            date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
            const utcString = date.toUTCString();
            expires = `; expires=${utcString}`;
        }
        document.cookie = name + '=' + (value || '') + expires + '; path=/';
    },
    delete: function(name) {
        document.cookie = name + '=; Max-Age=-99999999;';
    }
};
