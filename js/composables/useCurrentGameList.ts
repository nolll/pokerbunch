import { computed } from 'vue';
import { useCurrentGameListQuery } from '@/queries/gameQueries';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';

export default function useCurrentGameList(slug: string) {
  const currentGameListQuery = useCurrentGameListQuery(slug);

  return {
    currentGamesReady: computed((): boolean => !currentGameListQuery.isPending.value),
    currentGames: computed((): CurrentGameResponse[] => currentGameListQuery.data.value ?? []),
  };
}
