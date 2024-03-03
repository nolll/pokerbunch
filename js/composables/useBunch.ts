import { computed } from 'vue';
import { useBunchQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';

export default function useBunch(slug: string) {
  const bunchQuery = useBunchQuery(slug);

  const bunch = computed((): BunchResponse => {
    return bunchQuery.data.value!;
  });

  const bunchReady = computed((): boolean => {
    return !bunchQuery.isPending.value;
  });

  return {
    bunchReady: bunchReady,
    bunch: bunch,
  };
}
