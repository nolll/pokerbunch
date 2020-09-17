import { GameArchiveStoreGetters, GameArchiveStoreActions } from '@/store/helpers/GameArchiveStoreHelpers';

export default {
    computed: {
        $_games() {
            return this.$store.getters[GameArchiveStoreGetters.Games];
        },
        $_gamesReady() {
            return this.$store.getters[GameArchiveStoreGetters.GamesReady];
        },
        $_sortedGames() {
            return this.$store.getters[GameArchiveStoreGetters.SortedGames];
        },
        $_sortedPlayers() {
            return this.$store.getters[GameArchiveStoreGetters.SortedPlayers];
        },
        $_gameSortOrder() {
            return this.$store.getters[GameArchiveStoreGetters.GameSortOrder];
        },
        $_playerSortOrder() {
            return this.$store.getters[GameArchiveStoreGetters.PlayerSortOrder];
        },
        $_selectedYear() {
            return this.$store.getters[GameArchiveStoreGetters.SelectedYear];
        },
        $_years() {
            return this.$store.getters[GameArchiveStoreGetters.Years];
        },
        $_isPageNavExpanded() {
            return this.$store.getters[GameArchiveStoreGetters.IsPageNavExpanded];
        },
        $_isYearNavExpanded() {
            return this.$store.getters[GameArchiveStoreGetters.IsYearNavExpanded];
        },
        $_currentYearGames() {
            return this.$store.getters[GameArchiveStoreGetters.CurrentYearGames];
        },
        $_currentYearPlayers() {
            return this.$store.getters[GameArchiveStoreGetters.CurrentYearPlayers];
        },
        $_allYearsPlayers() {
            return this.$store.getters[GameArchiveStoreGetters.AllYearsPlayers];
        },
        $_currentYear() {
            return this.$store.getters[GameArchiveStoreGetters.CurrentYear];
        },
        $_hasGames() {
            return this.$store.getters[GameArchiveStoreGetters.HasGames];
        },
        $_routeYear() {
            if (this.$route.params.year)
                return parseInt(this.$route.params.year);
            return null;
        }
    },
    methods: {
        $_loadGames() {
            this.$store.dispatch(GameArchiveStoreActions.LoadGames, { slug: this.$route.params.slug });
            this.$store.dispatch(GameArchiveStoreActions.SelectYear, { year: this.$_routeYear });
        },
        $_sortGames(name) {
            this.$store.dispatch(GameArchiveStoreActions.SortGames, name);
        },
        $_sortPlayers(name) {
            this.$store.dispatch(GameArchiveStoreActions.SortPlayers, name);
        },
        $_toggleYearNav() {
            this.$store.dispatch(GameArchiveStoreActions.ToggleYearNav);
        },
        $_togglePageNav() {
            this.$store.dispatch(GameArchiveStoreActions.TogglePageNav);
        }
    }
};
