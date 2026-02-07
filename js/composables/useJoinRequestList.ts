import { computed } from 'vue';
import { useJoinRequestListQuery } from '@/queries/joinRequestQueries';
import { JoinRequestResponse } from '@/response/JoinRequestResponse';

export default function useJoinRequestList(slug: string) {
  const joinRequestListQuery = useJoinRequestListQuery(slug);

  const joinRequests = computed((): JoinRequestResponse[] => {
    return joinRequestListQuery.data.value ?? [];
  });

  const joinRequestsReady = computed((): boolean => {
    return !joinRequestListQuery.isPending.value;
  });

  return {
    joinRequests,
    joinRequestsReady,
  };
}
