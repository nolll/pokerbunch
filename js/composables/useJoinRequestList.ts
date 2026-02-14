import { computed } from 'vue';
import { useJoinRequestListQuery } from '@/queries/joinRequestQueries';
import { JoinRequestResponse } from '@/response/JoinRequestResponse';

export default function useJoinRequestList(slug: string) {
  const joinRequestListQuery = useJoinRequestListQuery(slug);

  return {
    joinRequests: computed((): JoinRequestResponse[] => joinRequestListQuery.data.value ?? []),
    joinRequestsReady: computed((): boolean => !joinRequestListQuery.isPending.value),
  };
}
