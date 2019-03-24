export default {
    init: function() {
        this.cancelUrl = this.getAttribute('data-cancel-url');

        this.addEventListener('click', event => {
            event.preventDefault();
            if (this.cancelUrl !== null) {
                location.href = this.cancelUrl;
            } else {
                history.back();
            }
        });
    }
};