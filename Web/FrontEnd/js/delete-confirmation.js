export default {
    init: function() {
        var message = this.getAttribute('data-message');
        if (!message) {
            message = 'Delete?';
        }
        this.addEventListener('click', e => {
            const response = window.confirm(message);
            if (!response)
                e.preventDefault();
            return response;
        });
    }
};