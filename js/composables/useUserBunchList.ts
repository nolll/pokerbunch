import { computed } from 'vue';
import { useUserBunchListQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';

export default function useUserBunchList(isSignedIn: boolean) {
  const userBunchListQuery = useUserBunchListQuery(isSignedIn);

  return {
    userBunches: computed((): BunchResponse[] => userBunchListQuery.data.value ?? []),
    userBunchesReady: computed((): boolean => !userBunchListQuery.isPending.value),
  };
}
