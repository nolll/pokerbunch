import { mapGetters } from 'vuex';
import { USER, BUNCH, CASHGAME, GAME_ARCHIVE, CURRENT_GAME, PLAYER } from '@/store-names';
import urls from '@/urls';

export default {
    data: function () {
        return {
            isUserRequired: true
        };
    },
    methods: {
        loadUser() {
            this.isUserRequired = false;
            this.$store.dispatch('user/loadUser');
        },
        loadUsers() {
            this.$store.dispatch('user/loadUsers');
        },
        requireUser() {
            this.isUserRequired = true;
            this.$store.dispatch('user/loadUser');
        },
        loadBunch() {
            this.$store.dispatch('bunch/loadBunch', { slug: this.$route.params.slug });
        },
        loadUserBunches() {
            this.$store.dispatch('bunch/loadUserBunches');
        },
        loadPlayers() {
            this.$store.dispatch('player/loadPlayers', { slug: this.$route.params.slug });
        },
        loadGames() {
            this.$store.dispatch('gameArchive/loadGames', { slug: this.$route.params.slug });
            this.$store.dispatch('gameArchive/selectYear', { year: this.routeYear });
        },
        loadCurrentGames() {
            this.$store.dispatch('currentGame/loadCurrentGames', { slug: this.$route.params.slug });
        },
        loadCashgame() {
            this.$store.dispatch('cashgame/loadCashgame', { id: this.$route.params.id });
        }
    },
    computed: {
        ...mapGetters(CURRENT_GAME, [
            'currentGamesReady'
        ]),
        ...mapGetters(CASHGAME, [
            'cashgameReady'
        ]),
        ...mapGetters(BUNCH, [
            'bunchReady',
            'userBunchesReady'
        ]),
        ...mapGetters(USER, [
            'userReady',
            'isSignedIn',
            'usersReady'
        ]),
        ...mapGetters(GAME_ARCHIVE, [
            'gamesReady'
        ]),
        ...mapGetters(PLAYER, [
            'playersReady'
        ]),
        routeYear() {
            if (this.$route.params.year)
                return parseInt(this.$route.params.year);
            return null;
        }
    },
    watch: {
        userReady: function (isUserReady) {
            if (isUserReady && this.isUserRequired && !this.isSignedIn) {
                this.$router.push(urls.auth.login);
            }
        }
    }
};
