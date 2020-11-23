<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="playerName" />
            </Block>
            <Block>
                <h2>Player Facts</h2>
            </Block>
            <Block>
                <ValueList>
                    <ValueListKey>Total Result</ValueListKey>
                    <ValueListValue><WinningsText :value="totalResult" /></ValueListValue>
                    <ValueListKey>Best Result</ValueListKey>
                    <ValueListValue><WinningsText :value="bestResult" /></ValueListValue>
                    <ValueListKey>Worst Result</ValueListKey>
                    <ValueListValue><WinningsText :value="worstResult" /></ValueListValue>
                    <ValueListKey>Games Played</ValueListKey>
                    <ValueListValue>{{gamesPlayed}}</ValueListValue>
                    <ValueListKey>Time Played</ValueListKey>
                    <ValueListValue><DurationText :value="timePlayed" /></ValueListValue>
                    <ValueListKey>Total Wins</ValueListKey>
                    <ValueListValue>{{totalWins}}</ValueListValue>
                    <ValueListKey>Current Streak</ValueListKey>
                    <ValueListValue>{{formattedCurrentStreak}}</ValueListValue>
                    <ValueListKey>Best Winning Streak</ValueListKey>
                    <ValueListValue>{{formattedWinningStreak}}</ValueListValue>
                    <ValueListKey>Worst Losing Streak</ValueListKey>
                    <ValueListValue>{{formattedLosingStreak}}</ValueListValue>
                </ValueList>
            </Block>

            <template slot="aside2">
                <Block>
                    <h2>User</h2>
                </Block>
                <Block v-if="hasUser">
                    <p>
                        <img :src="avatarUrl" alt="User avatar">
                    </p>
                    <p>
                        This player is a registered user.
                    </p>
                    <p>
                        <CustomButton :url="userUrl" text="View User Profile" />
                    </p>
                </Block>
                <Block v-else>
                    <p>
                        This player is not registered yet.
                    </p>
                    <p>
                        <CustomButton :url="inviteUrl" text="Invite Player" type="action" />
                    </p>
                </Block>
            </template>
        </PageSection>

        <PageSection v-if="canDelete">
            <Block>
                <h2>Delete Player</h2>
            </Block>
            <Block>
                <p>
                    <CustomButton @click="deletePlayer" text="Delete Player" type="action" />
                </p>
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, GameArchiveMixin, PlayerMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import Layout from '@/components/Layouts/Layout.vue';
    import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import ValueList from '@/components/Common/ValueList/ValueList.vue';
    import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
    import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
    import WinningsText from '@/components/Common/WinningsText.vue';
    import DurationText from '@/components/Common/DurationText.vue';
    import { Player } from '@/models/Player';
    import { ArchiveCashgame } from '@/models/ArchiveCashgame';
    import api from '@/api';
    import { User } from '@/models/User';
    
    @Component({
        components: {
            BunchNavigation,
            Block,
            CustomButton,
            CustomLink,
            DurationText,
            Layout,
            PageHeading,
            PageSection,
            ValueList,
            ValueListKey,
            ValueListValue,
            WinningsText
        }
    })
    export default class PlayerDetailsPage extends Mixins(
        BunchMixin,
        GameArchiveMixin,
        PlayerMixin,
        UserMixin
    ) {
        user: User | null = null;

        get hasUser(){
            return !!this.player?.userId;
        }

        get player(){
            return this.$_getPlayer(this.$route.params.id);
        }

        get playerName(){
            return this.player?.name;
        }

        get inviteUrl(){
            if(!this.player)
                return null;

            return urls.player.invite(this.player.id);
        }

        get userUrl(){
            if(this.user)
                return urls.user.details(this.user.userName);
        }

        get avatarUrl(){
            return this.user?.avatar;
        }

        get games(){
            return this.$_games.filter(g => this.isInGame(g))
        }

        get results(){
            let results = [];
            for(const game of this.games){
                for(const player of game.players){
                    if(player.id === this.player.id){
                        results.push(player);
                        break;
                    }
                }
            }
            return results;
        }

        get totalResult() {
            return this.results.reduce((acc, cur) => acc + cur.winnings, 0);
        }

        get bestResult() {
            let best: number | null = null;
            for (const result of this.results)
            {
                if (best == null || result.winnings > best)
                    best = result.winnings;
            }
            return best ?? 0;
        }

        get worstResult() {
            let worst: number | null = null;
            for (const result of this.results)
            {
                if (worst == null || result.winnings < worst)
                    worst = result.winnings;
            }
            return worst ?? 0;
        }

        get gamesPlayed() {
            return this.games.length;
        }

        get timePlayed() {
            return this.results.reduce((acc, cur) => acc + cur.timePlayed, 0);
        }

        get totalWins() {
            let count = 0;
            for(const game of this.games){
                if(game.isBestPlayer(this.player.id))
                    count += 1;
            }
            return count;
        }

        get currentStreak() {
            let lastStreak = 0;
            let currentStreak = 0;
            for (var result of this.results)
            {
                if (result.winnings >= 0)
                {
                    currentStreak++;
                }
                else
                {
                    currentStreak--;
                }
                if (Math.abs(currentStreak) < Math.abs(lastStreak))
                {
                    return lastStreak;
                }
                lastStreak = currentStreak;
            }
            return lastStreak;
        }

        get bestWinningStreak() {
            let bestStreak = 0;
            let currentStreak = 0;
            for (const result of this.results)
            {
                if (result.winnings >= 0)
                {
                    currentStreak++;
                    if (currentStreak > bestStreak)
                    {
                        bestStreak = currentStreak;
                    }
                }
                else
                {
                    currentStreak = 0;
                }
            }
            return bestStreak;
        }

        get formattedCurrentStreak(){
            if(this.currentStreak === 0)
                return '-';

            const wonOrLost = this.currentStreak > 0 ? 'Won' : 'Lost';
            const streak = Math.abs(this.currentStreak);
            return this.formatStreak(wonOrLost, streak);
        }

        get formattedWinningStreak(){
            return this.formatStreak('Won', this.bestWinningStreak);
        }

        get formattedLosingStreak(){
            return this.formatStreak('Lost', this.worstLosingStreak);
        }

        get worstLosingStreak() {
            let worstStreak = 0;
            let currentStreak = 0;
            for (var result of this.results)
            {
                if (result.winnings < 0)
                {
                    currentStreak++;
                    if (currentStreak > worstStreak)
                    {
                        worstStreak = currentStreak;
                    }
                }
                else
                {
                    currentStreak = 0;
                }
            }
            return worstStreak;
        }

        get ready() {
            return this.player != null && this.$_gamesReady;
        }

        get userReady() {
            return this.user != null;
        }

        get canDelete(){
            return this.results.length === 0;
        }

        private deletePlayer(){
            if (window.confirm('Do you want to delete this player?')) {
                this.$_deletePlayer(this.player);
                this.$router.push(urls.player.list(this.$_slug));
            }
        }

        private formatStreak(wonOrLost: string, gameCount: number){
            const gamesText = this.formatStreakGames(gameCount);
            return `${wonOrLost} in ${gameCount} ${gamesText}`;
        }

        private formatStreakGames(streak: number){
            return streak === 1 ? 'game' : 'games';
        }

        private isInGame(game: ArchiveCashgame){
            for(const p of game.players){
                if(p.id === this.player.id)
                    return true;
            }
            return false;
        }

        private async loadUser(){
            if(this.player?.userName){
                const response = await api.getUser(this.player.userName);
                this.user = response.status === 200
                    ? response.data
                    : null;
            }
        }

        async init() {
            this.$_requireUser();
            this.$_loadBunch();
            this.$_loadGames();
            await this.$_loadPlayers();
        }

        mounted() {
            this.init();
        }

        @Watch('player')
        playerChanged() {
            if(this.player)
                this.loadUser();
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>
