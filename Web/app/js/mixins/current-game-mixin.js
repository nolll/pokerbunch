import { CurrentGameStoreGetters, CurrentGameStoreActions } from '@/store/helpers/CurrentGameStoreHelpers';

export default {
    computed: {
        $_currentGames() {
            return this.$store.getters[CurrentGameStoreGetters.CurrentGames];
        },
        $_currentGamesReady() {
            return this.$store.getters[CurrentGameStoreGetters.CurrentGamesReady];
        }
    },
    methods: {
        $_loadCurrentGames() {
            this.$store.dispatch(CurrentGameStoreActions.LoadCurrentGames, { slug: this.$route.params.slug });
        }
    }
};
