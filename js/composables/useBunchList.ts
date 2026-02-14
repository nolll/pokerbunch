import { computed } from 'vue';
import { useBunchListQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';

export default function useBunchList() {
  const bunchListQuery = useBunchListQuery();

  return {
    bunches: computed((): BunchResponse[] => bunchListQuery.data.value ?? []),
    bunchesReady: computed((): boolean => !bunchListQuery.isPending.value),
  };
}
