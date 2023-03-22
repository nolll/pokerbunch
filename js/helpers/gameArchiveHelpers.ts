import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import dayjs from 'dayjs';

export const filterGames = (games: ArchiveCashgame[], selectedYear?: number | null) => {
  if (!selectedYear) return games;
  const selectedGames = [];
  for (const game of games) {
    const year = dayjs(game.startTime).year();
    if (year === selectedYear) {
      selectedGames.push(game);
    }
  }
  return selectedGames;
};

export const getYears = (games: ArchiveCashgame[]) => {
  const years: number[] = [];
  for (const game of games) {
    const year = dayjs(game.startTime).year();
    if (!years.includes(year)) {
      years.push(year);
    }
  }
  return years;
};
