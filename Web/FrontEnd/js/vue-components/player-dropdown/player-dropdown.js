import html from './player-dropdown.html';

export default {
    template: html,
    props: ['playerId', 'players'],
    methods: {
        changePlayer: function() {
            this.eventHub.$emit('change-player', this.selectedPlayerId);
        }
    },
    mounted: function() {
        this.selectedPlayerId = this.playerId;
    },
    data: function() {
        return {
            selectedPlayerId: null
        }
    }
};