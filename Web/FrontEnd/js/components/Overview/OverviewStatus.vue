<template>
    <div>
        <block>
            <h1 class="module-heading">Current Game</h1>
            <p>{{description}}</p>
        </block>
        <block>
            <custom-link :url="url" cssClasses="button button--action">{{linkText}}</custom-link>
        </block>
        <!--Remove dashboard for now-->
        <!--<block v-if="isRunning">
            There is also a <router-link :to="dashboardUrl">dashboard</router-link> for this game.
        </block>-->
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { OverviewRow } from '.';
    import urls from '@/urls';
    import { BUNCH, CURRENT_GAME } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { Block } from '@/components/Common';

    export default {
        components: {
            OverviewRow,
            CustomLink,
            Block
        },
        computed: {
            ...mapGetters(CURRENT_GAME, [
                'currentGames'
            ]),
            ...mapGetters(BUNCH, [
                'slug'
            ]),
            url() {
                return this.gameIsRunning ? this.runningGameUrl : this.addGameUrl;
            },
            addGameUrl() {
                return urls.cashgame.add(this.slug);
            },
            runningGameUrl() {
                return urls.cashgame.details(this.slug, this.runningGameId);
            },
            runningGameId() {
                if (this.currentGames.length === 0)
                    return 0;
                return this.currentGames[0].id;
            },
            gameIsRunning() {
                return this.currentGames.length > 0;
            },
            linkText() {
                return this.gameIsRunning ? 'Go to game' : 'Start a game';
            },
            description() {
                return this.gameIsRunning ? 'There is a game running' : 'No game is running at the moment';
            }
        }
    };
</script>

<style>

</style>
