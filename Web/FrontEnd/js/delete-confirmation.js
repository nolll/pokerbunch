function init() {
    var message = this.getAttribute('data-message');
    if(!message){
        message = 'Delete?';
    }
    this.addEventListener('click', function (e) {
        var response = window.confirm(message);
        if (!response)
            e.preventDefault();
        return response;
    });
}

export default {
    init: init
};