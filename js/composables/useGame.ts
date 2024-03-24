import { computed } from 'vue';
import { useGameQuery } from '@/queries/gameQueries';
import { DetailedCashgame } from '@/models/DetailedCashgame';

export default function useGame(slug: string) {
  const { data, isPending } = useGameQuery(slug);

  const game = computed((): DetailedCashgame => {
    return data.value!;
  });

  const gameReady = computed((): boolean => {
    return !isPending.value;
  });

  return {
    game,
    gameReady,
  };
}
