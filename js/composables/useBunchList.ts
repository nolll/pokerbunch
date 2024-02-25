import { computed } from 'vue';
import { useBunchListQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';

export default function useBunchList() {
  const bunchListQuery = useBunchListQuery();

  const bunches = computed((): BunchResponse[] => {
    return bunchListQuery.data.value ?? [];
  });

  const bunchesReady = computed((): boolean => {
    return !bunchListQuery.isPending.value;
  });

  return {
    bunches,
    bunchesReady,
  };
}
