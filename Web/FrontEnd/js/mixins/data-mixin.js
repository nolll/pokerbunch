'use strict';

export default {
    methods: {
        loadUser() {
            this.$store.dispatch('user/loadUser');
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
    }
};
