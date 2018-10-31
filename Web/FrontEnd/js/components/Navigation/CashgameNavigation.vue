<template>
    <nav class="cashgame-nav heading-nav is-expanded">
        <span v-on:click="togglePageNav">{{selectedPageName}}</span>
        <span v-on:click="toggleYearNav">{{selectedYear}}</span>
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
            <cashgame-navigation-item :url="getUrl()" text="All" :isSelected="isSelected()" v-on:selected="toggleYearNav" />
        </ul>
    </nav>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import { CashgameNavigationItem } from '.';

    export default {
        components: {
            CashgameNavigationItem
        },
        props: ['page', 'year'],
        computed: {
            ...mapState('gameArchive', ['selectedYear', 'isPageNavExpanded', 'isYearNavExpanded']),
            ...mapState('bunch', ['slug']),
            ...mapGetters('gameArchive', ['years']),
            computedYear() {
                if (this.year)
                    return this.year;
                return this.selectedYear;
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
            overviewUrl() {
                return '/cashgame/index/' + this.slug;
            },
            matrixUrl() {
                return '/cashgame/matrix/' + this.slug + '/' + this.computedYear;
            },
            toplistUrl() {
                return '/cashgame/toplist/' + this.slug + '/' + this.computedYear;
            },
            chartUrl() {
                return '/cashgame/chart/' + this.slug + '/' + this.computedYear;
            },
            listUrl() {
                return '/cashgame/list/' + this.slug + '/' + this.computedYear;
            },
            factsUrl() {
                return '/cashgame/facts/' + this.slug + '/' + this.computedYear;
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
                if (year)
                    return '/cashgame/' + this.page + '/' + this.slug + '/' + year;
                return '/cashgame/' + this.page + '/' + this.slug;
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
</script>

<style>
</style>
