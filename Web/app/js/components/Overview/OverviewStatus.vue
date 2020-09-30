<template>
    <div>
        <Block>
            <h1 class="module-heading">Current Game</h1>
            <p>{{description}}</p>
        </Block>
        <Block>
            <CustomLink :url="url" cssClasses="button button--action">{{linkText}}</CustomLink>
        </Block>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import Block from '@/components/Common/Block.vue';

    import { BunchMixin, CurrentGameMixin } from '@/mixins';

    @Component({
        components: {
            CustomLink,
            Block
        }
    })
    export default class OverviewStatus extends Mixins(
        BunchMixin,
        CurrentGameMixin
    ) {
        get url() {
            return this.gameIsRunning ? this.runningGameUrl : this.addGameUrl;
        }

        get addGameUrl() {
            return urls.cashgame.add(this.$_slug);
        }

        get runningGameUrl() {
            return urls.cashgame.details(this.$_slug, this.runningGameId);
        }

        get runningGameId() {
            if (this.$_currentGames.length === 0)
                return '0';
            return this.$_currentGames[0].id;
        }

        get gameIsRunning() {
            return this.$_currentGames.length > 0;
        }

        get linkText(): string {
            return this.gameIsRunning ? 'Go to game' : 'Start a game';
        }

        get description(): string {
            return this.gameIsRunning ? 'There is a game running' : 'No game is running at the moment';
        }
    }
</script>
