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
                'isRunning'
            ]),
            ...mapGetters(BUNCH, [
                'slug',
                'bunchReady'
            ]),
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
