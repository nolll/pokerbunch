import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { GameArchiveStoreMutations } from '@/store/helpers/GameArchiveStoreHelpers';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';
import gameSorter from '@/GameSorter';
import playerSorter from '@/PlayerSorter';
import archiveHelper from '@/ArchiveHelper';
import api from '@/api';
import dayjs from 'dayjs';

export default function useGameArchive() {
  const store = useStore();
  const route = useRoute();

  const games = computed((): ArchiveCashgame[] => {
    return store.state.gameArchive._games;
  });

  const gamesReady = computed((): boolean => {
    return store.state.gameArchive._ready;
  });

  const sortedGames = computed((): ArchiveCashgame[] => {
    const selectedGames = getSelectedGames(store.state.gameArchive._games, store.state.gameArchive._selectedYear);
    return gameSorter.sort(selectedGames, store.state.gameArchive._gameSortOrder);
  });

  const sortedPlayers = computed((): CashgameListPlayerData[] => {
    return playerSorter.sort(archiveHelper.getPlayers(sortedGames.value), store.state.gameArchive._playerSortOrder);
  });

  const gameSortOrder = computed((): CashgameSortOrder => {
    return store.state.gameArchive._gameSortOrder;
  });

  const playerSortOrder = computed((): CashgamePlayerSortOrder => {
    return store.state.gameArchive._playerSortOrder;
  });

  const selectedYear = computed((): number | undefined => {
    return store.state.gameArchive._selectedYear;
  });

  const years = computed((): number[] => {
    return getYears(store.state.gameArchive._games);
  });

  const currentYearGames = computed((): ArchiveCashgame[] => {
    const selectedGames = getSelectedGames(store.state.gameArchive._games, currentYear.value);
    return gameSorter.sort(selectedGames, CashgameSortOrder.Date);
  });

  const currentYearPlayers = computed((): CashgameListPlayerData[] => {
    return playerSorter.sort(archiveHelper.getPlayers(currentYearGames.value), CashgamePlayerSortOrder.Winnings);
  });

  const allYearsPlayers = computed((): CashgameListPlayerData[] => {
    return playerSorter.sort(archiveHelper.getPlayers(store.state.gameArchive._games), CashgamePlayerSortOrder.Winnings);
  });

  const currentYear = computed((): number | undefined => {
    const games = store.state.gameArchive._games;
    if (games > 0) {
      const latestGame = games[0];
      return dayjs(latestGame.startTime).year();
    }
    return undefined;
  });

  const hasGames = computed((): boolean => {
    return store.state.gameArchive._games.length > 0;
  });

  const routeYear = computed(() => {
    if (route.params.year) return parseInt(route.params.year as string, 10);
    return null;
  });

  const loadGames = async () => {
    const slug = route.params.slug as string;
    if (slug !== store.state._slug) {
      store.commit(GameArchiveStoreMutations.SetSlug, slug);
      const response = await api.getGames(slug);
      const games = response.data.map((o) => ArchiveCashgame.fromResponse(o));
      store.commit(GameArchiveStoreMutations.SetData, games);
    }
  };

  const selectYear = (year: number | undefined) => {
    store.commit(GameArchiveStoreMutations.SetSelectedYear, year);
  };

  const sortGames = (name: string) => {
    store.commit(GameArchiveStoreMutations.SetGameSortorder, name);
  };

  const sortPlayers = (name: string) => {
    store.commit(GameArchiveStoreMutations.SetPlayerSortorder, name);
  };

  function getSelectedGames(games: ArchiveCashgame[], selectedYear?: number | null) {
    if (!selectedYear) return games;
    const selectedGames = [];
    for (const game of games) {
      const year = dayjs(game.startTime).year();
      if (year === selectedYear) {
        selectedGames.push(game);
      }
    }
    return selectedGames;
  }

  function getYears(games: ArchiveCashgame[]) {
    const years: number[] = [];
    for (const game of games) {
      const year = dayjs(game.startTime).year();
      if (!years.includes(year)) {
        years.push(year);
      }
    }
    return years;
  }

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
    selectYear,
    sortGames,
    sortPlayers,
  };
}
