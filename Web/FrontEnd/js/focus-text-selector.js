import forms from './forms';

function init() {
    this.addEventListener('focus', function (e) {
        forms.selectAll(e.target);
    });
}

export default {
    init: init
};
