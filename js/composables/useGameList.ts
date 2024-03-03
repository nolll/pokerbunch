import { computed } from 'vue';
import { useGameListQuery } from '@/queries/gameQueries';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import dayjs from 'dayjs';
import gameSorter from '@/GameSorter';
import playerSorter from '@/PlayerSorter';
import archiveHelper from '@/ArchiveHelper';
import { CashgameSortOrder } from '@/models/CashgameSortOrder';
import { CashgameListPlayerData } from '@/models/CashgameListPlayerData';
import { CashgamePlayerSortOrder } from '@/models/CashgamePlayerSortOrder';

export default function useGameList(slug: string) {
  const { data, isPending } = useGameListQuery(slug);

  const allGames = computed((): ArchiveCashgame[] => {
    return data.value ?? [];
  });

  const hasGames = computed(() => {
    return allGames.value.length > 0;
  });

  const gamesReady = computed((): boolean => {
    return !isPending.value;
  });

  const getSelectedGames = (selectedYear?: number | null) => {
    if (!selectedYear) return allGames.value;
    const selectedGames = [];
    for (const game of allGames.value) {
      const year = dayjs(game.startTime).year();
      if (year === selectedYear) {
        selectedGames.push(game);
      }
    }
    return selectedGames;
  };

  const years = computed(() => {
    const years: number[] = [];
    for (const game of allGames.value) {
      const year = dayjs(game.startTime).year();
      if (!years.includes(year)) {
        years.push(year);
      }
    }
    return years;
  });

  const currentYear = computed(() => {
    if (allGames.value.length > 0) {
      const latestGame = allGames.value[0];
      return dayjs(latestGame.startTime).year();
    }
    return undefined;
  });

  return {
    allGames,
    hasGames,
    gamesReady,
    getSelectedGames,
    years,
    currentYear,
  };
}
