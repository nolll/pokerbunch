﻿import { mapState } from 'vuex';
import { USER } from '@/store-names';

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
            this.$store.dispatch('gameArchive/selectYear', { year: this.$route.params.year });
        },
        loadCurrentGame() {
            this.$store.dispatch('currentGame/loadCurrentGame', { slug: this.$route.params.slug });
        }
    },
    computed: {
        ...mapState(USER, {
                userReady: state => state.userReady,
                isSignedIn: state => state.isSignedIn
            }),
        loginUrl() {
            return '/auth/login';
        }
    },
    watch: {
        userReady: function (isUserReady) {
            if (isUserReady && this.isUserRequired && !this.isSignedIn) {
                window.location.href = this.loginUrl;
            }
        }
    }
};
