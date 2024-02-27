import { computed } from 'vue';
import { useBunchQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';
import useParams from './useParams';

export default function useBunch() {
  const params = useParams();
  const bunchQuery = useBunchQuery(params.slug.value);

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
