import { computed, Ref } from 'vue';
import { useGameQuery } from '@/queries/gameQueries';
import { DetailedCashgame } from '@/models/DetailedCashgame';

export default function useGame(slug: string, isEnabled: Ref<boolean>) {
  const { data, isPending } = useGameQuery(slug, isEnabled);

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
