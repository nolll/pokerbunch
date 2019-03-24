import forms from './forms';

export default {
    init: function() {
        this.addEventListener('focus', e => {
            forms.selectAll(e.target);
        });
    }
};
