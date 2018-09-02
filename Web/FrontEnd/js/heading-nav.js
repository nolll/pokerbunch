function HeadingNav(el) {
    var me = this;
    me.el = el;
    me.isExpanded = false;

    el.addEventListener('click', function (e) {
        e.stopPropagation();
        if (!me.isExpanded) {
            e.preventDefault();
            me.isExpanded = true;
            me.el.classList.add('is-expanded');
        }
    });

    document.addEventListener('click', function (e) {
        if (me.isExpanded) {
            me.isExpanded = false;
            me.el.classList.remove('is-expanded');
        }
    });
}

function init() {
    return new HeadingNav(this);
}

export default {
    init: init
};
