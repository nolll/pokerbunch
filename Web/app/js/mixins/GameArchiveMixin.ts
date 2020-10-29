import { Component, Vue } from 'vue-property-decorator';
import { GameArchiveStoreGetters, GameArchiveStoreActions } from '@/store/helpers/GameArchiveStoreHelpers';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

@Component
export class GameArchiveMixin extends Vue {
    protected get $_games(): ArchiveCashgame[] {
        return this.$store.getters[GameArchiveStoreGetters.Games];
    }

    protected get $_gamesReady(): boolean {
        return this.$store.getters[GameArchiveStoreGetters.GamesReady];
    }

    protected get $_sortedGames(): ArchiveCashgame[] {
        return this.$store.getters[GameArchiveStoreGetters.SortedGames];
    }

    protected get $_sortedPlayers(): CashgameListPlayerData[] {
        return this.$store.getters[GameArchiveStoreGetters.SortedPlayers];
    }

    protected get $_gameSortOrder(): CashgameSortOrder {
        return this.$store.getters[GameArchiveStoreGetters.GameSortOrder];
    }

    protected get $_playerSortOrder(): CashgamePlayerSortOrder {
        return this.$store.getters[GameArchiveStoreGetters.PlayerSortOrder];
    }

    protected get $_selectedYear(): number | null {
        return this.$store.getters[GameArchiveStoreGetters.SelectedYear];
    }

    protected get $_years(): number[] {
        return this.$store.getters[GameArchiveStoreGetters.Years];
    }

    protected get $_isPageNavExpanded(): boolean {
        return this.$store.getters[GameArchiveStoreGetters.IsPageNavExpanded];
    }

    protected get $_isYearNavExpanded(): boolean {
        return this.$store.getters[GameArchiveStoreGetters.IsYearNavExpanded];
    }

    protected get $_currentYearGames(): ArchiveCashgame[] {
        return this.$store.getters[GameArchiveStoreGetters.CurrentYearGames];
    }

    protected get $_currentYearPlayers(): CashgameListPlayerData[] {
        return this.$store.getters[GameArchiveStoreGetters.CurrentYearPlayers];
    }

    protected get $_allYearsPlayers(): CashgameListPlayerData[] {
        return this.$store.getters[GameArchiveStoreGetters.AllYearsPlayers];
    }

    protected get $_currentYear(): number | null {
        return this.$store.getters[GameArchiveStoreGetters.CurrentYear];
    }

    protected get $_hasGames(): boolean {
        return this.$store.getters[GameArchiveStoreGetters.HasGames];
    }

    protected get $_routeYear() {
        if (this.$route.params.year)
            return parseInt(this.$route.params.year, 10);
        return null;
    }

    protected $_loadGames() {
        this.$store.dispatch(GameArchiveStoreActions.LoadGames, { slug: this.$route.params.slug });
        this.$store.dispatch(GameArchiveStoreActions.SelectYear, { year: this.$_routeYear });
    }

    protected $_sortGames(name: string) {
        this.$store.dispatch(GameArchiveStoreActions.SortGames, name);
    }

    protected $_sortPlayers(name: string) {
        this.$store.dispatch(GameArchiveStoreActions.SortPlayers, name);
    }

    protected $_toggleYearNav() {
        this.$store.dispatch(GameArchiveStoreActions.ToggleYearNav);
    }

    protected $_togglePageNav() {
        this.$store.dispatch(GameArchiveStoreActions.TogglePageNav);
    }

    protected $_closeYearNav() {
        this.$store.dispatch(GameArchiveStoreActions.CloseYearNav);
    }

    protected $_closePageNav() {
        this.$store.dispatch(GameArchiveStoreActions.ClosePageNav);
    }

    $store: any;
}
