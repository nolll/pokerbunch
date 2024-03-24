import { computed } from 'vue';
import { useCurrentGameListQuery } from '@/queries/gameQueries';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';

export default function useCurrentGameList(slug: string) {
  const currentGameListQuery = useCurrentGameListQuery(slug);

  const currentGames = computed((): CurrentGameResponse[] => {
    return currentGameListQuery.data.value ?? [];
  });

  const currentGamesReady = computed((): boolean => {
    return !currentGameListQuery.isPending.value;
  });

  return {
    currentGamesReady,
    currentGames,
  };
}
