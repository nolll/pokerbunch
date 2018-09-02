import html from './game-button.html';

export default {
    template: html,
    props: ['text', 'icon'],
    computed: {
        iconCssClass: function() {
            return 'icon-' + this.icon;
        }
    }
};