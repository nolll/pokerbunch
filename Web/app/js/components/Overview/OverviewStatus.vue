<template>
    <div>
        <block>
            <h1 class="module-heading">Current Game</h1>
            <p>{{description}}</p>
        </block>
        <block>
            <custom-link :url="url" cssClasses="button button--action">{{linkText}}</custom-link>
        </block>
    </div>
</template>

<script>
    import { OverviewRow } from '.';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { Block } from '@/components/Common';
    import { BunchMixin, CurrentGameMixin } from '@/mixins';

    export default {
        components: {
            OverviewRow,
            CustomLink,
            Block
        },
        mixins: [
            BunchMixin,
            CurrentGameMixin
        ],
        computed: {
            url() {
                return this.gameIsRunning ? this.runningGameUrl : this.addGameUrl;
            },
            addGameUrl() {
                return urls.cashgame.add(this.$_slug);
            },
            runningGameUrl() {
                return urls.cashgame.details(this.$_slug, this.runningGameId);
            },
            runningGameId() {
                if (this.$_currentGames.length === 0)
                    return 0;
                return this.$_currentGames[0].id;
            },
            gameIsRunning() {
                return this.$_currentGames.length > 0;
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
