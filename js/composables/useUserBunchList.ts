import { computed } from 'vue';
import { useUserBunchListQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';

export default function useUserBunchList(isSignedIn: boolean) {
  const userBunchListQuery = useUserBunchListQuery(isSignedIn);

  const userBunches = computed((): BunchResponse[] => {
    return userBunchListQuery.data.value ?? [];
  });

  const userBunchesReady = computed((): boolean => {
    return !userBunchListQuery.isPending.value;
  });

  return {
    userBunches,
    userBunchesReady,
  };
}
