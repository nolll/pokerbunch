<template>
    <tr class="table-list__row">
        <td class="table-list__cell table-list--sortable__base-column" :class="dateSortClasses">
            <custom-link :url="url">{{displayDate}}</custom-link>
        </td>
        <td class="table-list__cell table-list__cell--numeric" :class="playerCountSortClasses">{{game.playerCount}}</td>
        <td class="table-list__cell">{{game.location.name}}</td>
        <td class="table-list__cell" :class="durationSortClasses">{{duration}}</td>
        <td class="table-list__cell table-list__cell--numeric" :class="turnoverSortClasses">{{formattedTurnover}}</td>
        <td class="table-list__cell table-list__cell--numeric" :class="averageBuyinSortClasses">{{formattedAverageBuyin}}</td>
    </tr>
</template>

<script>
    import { mapGetters } from 'vuex';
    import moment from 'moment';
    import { FormatMixin } from '@/mixins';
    import urls from '@/urls';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        mixins: [
            FormatMixin
        ],
        components: {
            CustomLink
        },
        props: ['game'],
        computed: {
            ...mapGetters(BUNCH, [
                'slug'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'gameSortOrder'
            ]),
            url() {
                return urls.cashgame.details(this.slug, this.game.id);
            },
            displayDate() {
                return moment(this.game.date).format('MMM D');
            },
            dateSortClasses() {
                return this.getSortCssClasses(this.orderBy, 'date');
            },
            playerCountSortClasses() {
                return this.getSortCssClasses('playercount');
            },
            durationSortClasses() {
                return this.getSortCssClasses('duration');
            },
            turnoverSortClasses() {
                return this.getSortCssClasses(this.orderBy, 'turnover');
            },
            averageBuyinSortClasses() {
                return this.getSortCssClasses(this.orderBy, 'averagebuyin');
            },
            duration() {
                return this.formatTime(this.game.duration);
            },
            formattedAverageBuyin() {
                return this.formatCurrency(this.game.averageBuyin);
            },
            formattedTurnover() {
                return this.formatCurrency(this.game.turnover);
            }
        },
        methods: {
            isSortedBy(col) {
                return this.orderBy === col;
            },
            getSortCssClasses(col) {
                return {
                    'table-list--sortable__sort-item': this.isSortedBy(col)
                }
            }
        }
    };
</script>

<style>
</style>
