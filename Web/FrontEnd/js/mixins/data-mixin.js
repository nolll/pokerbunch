﻿import { mapGetters } from 'vuex';
import { USER, BUNCH, GAME_ARCHIVE, CURRENT_GAME } from '@/store-names';
import urls from '@/urls';

export default {
    data: function () {
        return {
            isUserRequired: true
        };
    },
    methods: {
        loadUser() {
            this.$store.dispatch('user/loadUser');
        },
        requireUser() {
            this.$store.dispatch('user/requireUser');
        },
        loadBunch() {
            this.$store.dispatch('bunch/loadBunch', { slug: this.$route.params.slug });
        },
        loadGames() {
            this.$store.dispatch('gameArchive/loadGames', { slug: this.$route.params.slug });
            this.$store.dispatch('gameArchive/selectYear', { year: this.routeYear });
        },
        loadCurrentGame() {
            this.$store.dispatch('currentGame/loadCurrentGame', { slug: this.$route.params.slug });
        }
    },
    computed: {
        ...mapGetters(CURRENT_GAME, [
            'currentGameReady'
        ]),
        ...mapGetters(BUNCH, [
            'bunchReady'
        ]),
        ...mapGetters(USER, [
            'userReady',
            'isSignedIn'
        ]),
        ...mapGetters(GAME_ARCHIVE, [
            'gamesReady'
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
                this.$router.push(urls.login());
            }
        }
    }
};