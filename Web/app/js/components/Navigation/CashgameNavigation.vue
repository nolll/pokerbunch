<template>
    <div class="heading-nav-container">
        <nav class="cashgame-nav heading-nav is-expanded">
            <span v-on:click="$_togglePageNav" class="heading-nav__sub-nav">{{selectedPageName}}</span>
            <span v-show="isYearNavEnabled">| <span v-on:click="$_toggleYearNav" class="heading-nav__sub-nav">{{presentationYear}}</span></span>
            <ul v-show="$_isPageNavExpanded">
                <CashgameNavigationItem :url="overviewUrl" text="Overview" :isSelected="isOverviewSelected" v-on:selected="onSelected" />
                <CashgameNavigationItem :url="matrixUrl" text="Matrix" :isSelected="isMatrixSelected" v-on:selected="onSelected" />
                <CashgameNavigationItem :url="toplistUrl" text="Toplist" :isSelected="isToplistSelected" v-on:selected="onSelected" />
                <CashgameNavigationItem :url="chartUrl" text="Chart" :isSelected="isChartSelected" v-on:selected="onSelected" />
                <CashgameNavigationItem :url="listUrl" text="List" :isSelected="isListSelected" v-on:selected="onSelected" />
                <CashgameNavigationItem :url="factsUrl" text="Facts" :isSelected="isFactsSelected" v-on:selected="onSelected" />
            </ul>
            <ul v-show="$_isYearNavExpanded">
                <CashgameNavigationItem v-for="year in $_years" :key="year" :url="getUrl(year)" :text="year" :isSelected="isSelected(year)" v-on:selected="onSelected" />
                <CashgameNavigationItem :url="getUrl()" :text="allYearsText" :isSelected="isSelected()" v-on:selected="onSelected" />
            </ul>
        </nav>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import CashgameNavigationItem from './CashgameNavigationItem.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import { CashgamePage } from '@/models/CashgamePage';

    @Component({
        components: {
            CashgameNavigationItem
        }
    })
    export default class CashgameNavigation extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ) {
        @Prop() readonly page!: CashgamePage;
        @Prop() readonly year?: number | null | undefined;

        allYearsText = 'All years';
            get computedYear() {
                if (this.year)
                    return this.year;
                return this.$_selectedYear;
            }

            get presentationYear() {
                return this.$_selectedYear || this.allYearsText;
            }

            get selectedPageName() {
                if (this.page === CashgamePage.Matrix)
                    return 'Matrix';
                if (this.page === CashgamePage.Toplist)
                    return 'Toplist';
                if (this.page === CashgamePage.Chart)
                    return 'Chart';
                if (this.page === CashgamePage.List)
                    return 'List';
                if (this.page === CashgamePage.Facts)
                    return 'Facts';
                return 'Overview'
            }

            get isYearNavEnabled() {
                return this.page !== CashgamePage.Overview
            }

            get overviewUrl() {
                return buildUrl(CashgamePage.Overview, this.$_slug);
            }

            get matrixUrl() {
                return buildUrl(CashgamePage.Matrix, this.$_slug, this.computedYear);
            }

            get toplistUrl() {
                return buildUrl(CashgamePage.Toplist, this.$_slug, this.computedYear);
            }

            get chartUrl() {
                return buildUrl(CashgamePage.Chart, this.$_slug, this.computedYear);
            }

            get listUrl() {
                return buildUrl(CashgamePage.List, this.$_slug, this.computedYear);
            }

            get factsUrl() {
                return buildUrl(CashgamePage.Facts, this.$_slug, this.computedYear);
            }

            get isOverviewSelected() {
                return this.page === CashgamePage.Overview;
            }

            get isMatrixSelected() {
                return this.page === CashgamePage.Matrix;
            }

            get isToplistSelected() {
                return this.page === CashgamePage.Toplist;
            }

            get isChartSelected() {
                return this.page === CashgamePage.Chart;
            }

            get isListSelected() {
                return this.page === CashgamePage.List;
            }

            get isFactsSelected() {
                return this.page === CashgamePage.Facts;
            }

            getUrl(year?: number) {
                return buildUrl(this.page, this.$_slug, year)
            }
            
            isSelected(year: number) {
                if (!year && !this.$_selectedYear)
                    return true;
                return year === this.$_selectedYear;
            }

            onSelected(url: string) {
                this.$_closePageNav();
                this.$_closeYearNav();
                if(this.$router.currentRoute.fullPath !== url)
                    this.$router.push(url);
            }
    }

    function buildUrl(page: CashgamePage, slug: string, year?: number | null | undefined ) {
        var pageName = page.toLowerCase();
        return year
            ? `/cashgame/${pageName}/${slug}/${year}`
            : `/cashgame/${pageName}/${slug}`
    }
</script>
