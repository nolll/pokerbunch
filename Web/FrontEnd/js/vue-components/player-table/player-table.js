import html from './player-table.html';
import gameService from '../../game-service';

export default {
    template: html,
    props: ['players', 'currencyFormat'],
    created: function() {
    },
    computed: {
        totalBuyin: function() {
            return gameService.getTotalBuyin(this.players);
        },
        totalStacks: function() {
            return gameService.getTotalStacks(this.players);
        },
        formattedTotalBuyin: function() {
            return this.formatCurrency(this.totalBuyin);
        },
        formattedTotalStacks: function() {
            return this.formatCurrency(this.totalStacks);
        }
    },
    methods: {
        formatCurrency: function(amount) {
            return this.$options.filters.customCurrency(amount, this.currencyFormat);
        }
    }
};