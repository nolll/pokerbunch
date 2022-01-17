import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { GameArchiveStoreActions, GameArchiveStoreGetters } from '@/store/helpers/GameArchiveStoreHelpers';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

export default function useGameArchive() {
  const store = useStore();
  const route = useRoute();

  const games = computed((): ArchiveCashgame[] => {
    return store.getters[GameArchiveStoreGetters.Games];
  });

  const gamesReady = computed((): boolean => {
    return store.getters[GameArchiveStoreGetters.GamesReady];
  });

  const sortedGames = computed((): ArchiveCashgame[] => {
    return store.getters[GameArchiveStoreGetters.SortedGames];
  });

  const sortedPlayers = computed((): CashgameListPlayerData[] => {
    return store.getters[GameArchiveStoreGetters.SortedPlayers];
  });

  const gameSortOrder = computed((): CashgameSortOrder => {
    return store.getters[GameArchiveStoreGetters.GameSortOrder];
  });

  const playerSortOrder = computed((): CashgamePlayerSortOrder => {
    return store.getters[GameArchiveStoreGetters.PlayerSortOrder];
  });

  const selectedYear = computed((): number | undefined => {
    return store.getters[GameArchiveStoreGetters.SelectedYear];
  });

  const years = computed((): number[] => {
    return store.getters[GameArchiveStoreGetters.Years];
  });

  const currentYearGames = computed((): ArchiveCashgame[] => {
    return store.getters[GameArchiveStoreGetters.CurrentYearGames];
  });

  const currentYearPlayers = computed((): CashgameListPlayerData[] => {
    return store.getters[GameArchiveStoreGetters.CurrentYearPlayers];
  });

  const allYearsPlayers = computed((): CashgameListPlayerData[] => {
    return store.getters[GameArchiveStoreGetters.AllYearsPlayers];
  });

  const currentYear = computed((): number | undefined => {
    return store.getters[GameArchiveStoreGetters.CurrentYear];
  });

  const hasGames = computed((): boolean => {
    return store.getters[GameArchiveStoreGetters.HasGames];
  });

  const routeYear = computed(() => {
    if (route.params.year) return parseInt(route.params.year as string, 10);
    return null;
  });

  const loadGames = () => {
    store.dispatch(GameArchiveStoreActions.LoadGames, { slug: route.params.slug });
    store.dispatch(GameArchiveStoreActions.SelectYear, { year: routeYear.value });
  };

  const sortGames = (name: string) => {
    store.dispatch(GameArchiveStoreActions.SortGames, name);
  };

  const sortPlayers = (name: string) => {
    store.dispatch(GameArchiveStoreActions.SortPlayers, name);
  };

  return {
    games,
    gamesReady,
    sortedGames,
    sortedPlayers,
    gameSortOrder,
    playerSortOrder,
    selectedYear,
    years,
    currentYearGames,
    currentYearPlayers,
    allYearsPlayers,
    currentYear,
    hasGames,
    routeYear,
    loadGames,
    sortGames,
    sortPlayers,
  };
}
