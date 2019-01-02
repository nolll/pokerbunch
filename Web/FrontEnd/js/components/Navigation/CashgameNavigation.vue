<template>
    <nav class="cashgame-nav heading-nav is-expanded">
        <span v-on:click="togglePageNav" class="heading-nav__sub-nav">{{selectedPageName}}</span>
        <span v-show="isYearNavEnabled">| <span v-on:click="toggleYearNav" class="heading-nav__sub-nav">{{presentationYear}}</span></span>
        <ul v-show="isPageNavExpanded">
            <cashgame-navigation-item :url="overviewUrl" text="Overview" :isSelected="isOverviewSelected" v-on:selected="togglePageNav" />
            <cashgame-navigation-item :url="matrixUrl" text="Matrix" :isSelected="isMatrixSelected" v-on:selected="togglePageNav" />
            <cashgame-navigation-item :url="toplistUrl" text="Toplist" :isSelected="isToplistSelected" v-on:selected="togglePageNav" />
            <cashgame-navigation-item :url="chartUrl" text="Chart" :isSelected="isChartSelected" v-on:selected="togglePageNav" />
            <cashgame-navigation-item :url="listUrl" text="List" :isSelected="isListSelected" v-on:selected="togglePageNav" />
            <cashgame-navigation-item :url="factsUrl" text="Facts" :isSelected="isFactsSelected" v-on:selected="togglePageNav" />
        </ul>
        <ul v-show="isYearNavExpanded">
            <cashgame-navigation-item v-for="year in years" :key="year" :url="getUrl(year)" :text="year" :isSelected="isSelected(year)" v-on:selected="toggleYearNav" />
            <cashgame-navigation-item :url="getUrl()" :text="allYearsText" :isSelected="isSelected()" v-on:selected="toggleYearNav" />
        </ul>
    </nav>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { CashgameNavigationItem } from '.';
    import { BUNCH, GAME_ARCHIVE } from '@/store-names';

    export default {
        components: {
            CashgameNavigationItem
        },
        props: ['page', 'year'],
        data: function () {
            return {
                allYearsText: 'All years'
            };
        },
        computed: {
            ...mapGetters(BUNCH, [
                'slug'
            ]),
            ...mapGetters(GAME_ARCHIVE, [
                'selectedYear',
                'years',
                'isPageNavExpanded',
                'isYearNavExpanded'
            ]),
            computedYear() {
                if (this.year)
                    return this.year;
                return this.selectedYear;
            },
            presentationYear() {
                return this.selectedYear || this.allYearsText;
            },
            selectedPageName() {
                if (this.page === 'matrix')
                    return 'Matrix';
                if (this.page === 'toplist')
                    return 'Toplist';
                if (this.page === 'chart')
                    return 'Chart';
                if (this.page === 'list')
                    return 'List';
                if (this.page === 'facts')
                    return 'Facts';
                return 'Overview'
            },
            isYearNavEnabled() {
                return this.page !== 'overview'
            },
            overviewUrl() {
                return buildUrl('index', this.slug);
            },
            matrixUrl() {
                return buildUrl('matrix', this.slug, this.computedYear);
            },
            toplistUrl() {
                return buildUrl('toplist', this.slug, this.computedYear);
            },
            chartUrl() {
                return buildUrl('chart', this.slug, this.computedYear);
            },
            listUrl() {
                return buildUrl('list', this.slug, this.computedYear);
            },
            factsUrl() {
                return buildUrl('facts', this.slug, this.computedYear);
            },
            isOverviewSelected() {
                return this.page === 'overview';
            },
            isMatrixSelected() {
                return this.page === 'matrix';
            },
            isToplistSelected() {
                return this.page === 'toplist';
            },
            isChartSelected() {
                return this.page === 'chart';
            },
            isListSelected() {
                return this.page === 'list';
            },
            isFactsSelected() {
                return this.page === 'facts';
            }
        },
        methods: {
            getUrl(year) {
                return buildUrl(this.page, this.slug, year)
            },
            isSelected(year) {
                if (!year && !this.selectedYear)
                    return true;
                return year === this.selectedYear;
            },
            toggleYearNav() {
                this.$store.dispatch('gameArchive/toggleYearNav');
            },
            togglePageNav() {
                this.$store.dispatch('gameArchive/togglePageNav');
            }
        }
    };

    function buildUrl(page, slug, year) {
        var url = '/cashgame/' + page + '/' + slug;
        if (year)
            url += '/' + year;
        return url;
    }
</script>

<style>
</style>
