<template>
    <div class="heading-nav-container">
        <nav class="cashgame-nav heading-nav is-expanded">
            <span v-on:click="$_togglePageNav" class="heading-nav__sub-nav">{{selectedPageName}}</span>
            <span v-show="isYearNavEnabled">| <span v-on:click="$_toggleYearNav" class="heading-nav__sub-nav">{{presentationYear}}</span></span>
            <ul v-show="$_isPageNavExpanded">
                <cashgame-navigation-item :url="overviewUrl" text="Overview" :isSelected="isOverviewSelected" v-on:selected="$_togglePageNav" />
                <cashgame-navigation-item :url="matrixUrl" text="Matrix" :isSelected="isMatrixSelected" v-on:selected="$_togglePageNav" />
                <cashgame-navigation-item :url="toplistUrl" text="Toplist" :isSelected="isToplistSelected" v-on:selected="$_togglePageNav" />
                <cashgame-navigation-item :url="chartUrl" text="Chart" :isSelected="isChartSelected" v-on:selected="$_togglePageNav" />
                <cashgame-navigation-item :url="listUrl" text="List" :isSelected="isListSelected" v-on:selected="$_togglePageNav" />
                <cashgame-navigation-item :url="factsUrl" text="Facts" :isSelected="isFactsSelected" v-on:selected="$_togglePageNav" />
            </ul>
            <ul v-show="$_isYearNavExpanded">
                <cashgame-navigation-item v-for="year in $_years" :key="year" :url="getUrl(year)" :text="year" :isSelected="isSelected(year)" v-on:selected="$_toggleYearNav" />
                <cashgame-navigation-item :url="getUrl()" :text="allYearsText" :isSelected="isSelected()" v-on:selected="$_toggleYearNav" />
            </ul>
        </nav>
    </div>
</template>

<script>
    import { CashgameNavigationItem } from '.';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';

    export default {
        components: {
            CashgameNavigationItem
        },
        mixins: [
            BunchMixin,
            GameArchiveMixin
        ],
        props: ['page', 'year'],
        data: function () {
            return {
                allYearsText: 'All years'
            };
        },
        computed: {
            computedYear() {
                if (this.year)
                    return this.year;
                return this.$_selectedYear;
            },
            presentationYear() {
                return this.$_selectedYear || this.allYearsText;
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
                return buildUrl('index', this.$_slug);
            },
            matrixUrl() {
                return buildUrl('matrix', this.$_slug, this.computedYear);
            },
            toplistUrl() {
                return buildUrl('toplist', this.$_slug, this.computedYear);
            },
            chartUrl() {
                return buildUrl('chart', this.$_slug, this.computedYear);
            },
            listUrl() {
                return buildUrl('list', this.$_slug, this.computedYear);
            },
            factsUrl() {
                return buildUrl('facts', this.$_slug, this.computedYear);
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
                return buildUrl(this.page, this.$_slug, year)
            },
            isSelected(year) {
                if (!year && !this.$_selectedYear)
                    return true;
                return year === this.$_selectedYear;
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
