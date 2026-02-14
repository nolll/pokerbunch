import { computed, Ref } from 'vue';
import { useGameQuery } from '@/queries/gameQueries';
import { DetailedCashgame } from '@/models/DetailedCashgame';

export default function useGame(slug: string, isEnabled: Ref<boolean>) {
  const { data, isPending } = useGameQuery(slug, isEnabled);

  return {
    game: computed((): DetailedCashgame => data.value!),
    gameReady: computed((): boolean => !isPending.value),
  };
}
