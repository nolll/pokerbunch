<template>
    <div>
        <div class="block">
            <h1 class="module-heading">Current Game</h1>
            <p>{{description}}</p>
        </div>
        <div class="block">
            <a :href="url" class="button button--action">{{linkText}}</a>
        </div>
        <!--Remove dashboard for now-->
        <!--<div class="block" v-if="isRunning">
            There is also a <router-link :to="dashboardUrl">dashboard</router-link> for this game.
        </div>-->
    </div>
</template>

<script>
    import { mapState } from 'vuex';
    import { OverviewRow } from ".";

    export default {
        components: {
            OverviewRow
        },
        computed: {
            ...mapState('bunch', {
                bunchReady: state => state.bunchReady,
                slug: state => state.slug
            }),
            ...mapState('currentGame', {
                isRunning: state => state.isRunning
            }),
            url() {
                return this.isRunning ? '/cashgame/running/' + this.slug : '/cashgame/add/' + this.slug;
            },
            dashboardUrl() {
                return '/cashgame/dashboard/' + this.slug;
            },
            linkText() {
                return this.isRunning ? 'Go to game' : 'Start a game';
            },
            description() {
                return this.isRunning ? 'There is a game running' : 'No game is running at the moment';
            },
            ready() {
                return this.bunchReady;
            }
        }
    };
</script>

<style>

</style>
