<template>
    <nav class="heading-nav">
        <h1 class="page-heading">{{selectedPageName}}</h1>
        <ul>
            <CashgameNavigationItem :url="overviewUrl" text="Overview" :isSelected="isOverviewSelected" v-on:selected="onSelected" />
            <CashgameNavigationItem :url="matrixUrl" text="Matrix" :isSelected="isMatrixSelected" v-on:selected="onSelected" />
            <CashgameNavigationItem :url="toplistUrl" text="Toplist" :isSelected="isToplistSelected" v-on:selected="onSelected" />
            <CashgameNavigationItem :url="chartUrl" text="Chart" :isSelected="isChartSelected" v-on:selected="onSelected" />
            <CashgameNavigationItem :url="listUrl" text="List" :isSelected="isListSelected" v-on:selected="onSelected" />
            <CashgameNavigationItem :url="factsUrl" text="Facts" :isSelected="isFactsSelected" v-on:selected="onSelected" />
        </ul>
        <YearDropdown class="year-dropdown" v-model="selectedYear" :years="years" v-on:input="onSelectedYear" v-if="isYearNavEnabled" />
    </nav>
</template>

<script lang="ts">
    import { Component, Prop, Mixins, Watch } from 'vue-property-decorator';
    import CashgameNavigationItem from './CashgameNavigationItem.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import { CashgamePage } from '@/models/CashgamePage';
    import YearDropdown from '@/components/YearDropdown.vue';
    import urls from '@/urls';

    @Component({
        components: {
            CashgameNavigationItem,
            YearDropdown
        }
    })
    export default class CashgameNavigation extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ) {
        selectedYear: number | null | undefined = null;

        @Prop() readonly page!: CashgamePage;

        get years(){
            return this.$_years;
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
            return urls.cashgame.archive(CashgamePage.Overview, this.$_slug);
        }

        get matrixUrl() {
            return urls.cashgame.archive(CashgamePage.Matrix, this.$_slug, this.selectedYear);
        }

        get toplistUrl() {
            return urls.cashgame.archive(CashgamePage.Toplist, this.$_slug, this.selectedYear);
        }

        get chartUrl() {
            return urls.cashgame.archive(CashgamePage.Chart, this.$_slug, this.selectedYear);
        }

        get listUrl() {
            return urls.cashgame.archive(CashgamePage.List, this.$_slug, this.selectedYear);
        }

        get factsUrl() {
            return urls.cashgame.archive(CashgamePage.Facts, this.$_slug, this.selectedYear);
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
            return urls.cashgame.archive(this.page, this.$_slug, year)
        }

        onSelected(url: string) {
            if(this.$router.currentRoute.fullPath !== url)
                this.$router.push(url);
        }

        mounted(){
            this.selectedYear = this.$_selectedYear || this.$_currentYear;
        }

        onSelectedYear(){
            this.$router.push(urls.cashgame.archive(this.page, this.$_slug, this.selectedYear));
        }
    }
</script>

<style lang="less" scoped>
    .year-dropdown{
        margin-top: 10px;
    }
</style>