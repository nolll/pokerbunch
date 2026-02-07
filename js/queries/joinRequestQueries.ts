import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { joinRequestListKey } from './queryKeys';
import { JoinRequestResponse } from '@/response/JoinRequestResponse';

export const useJoinRequestListQuery = (slug: string) => {
  return useQuery({
    queryKey: joinRequestListKey(slug),
    queryFn: async (): Promise<JoinRequestResponse[]> => {
      const response = await api.getJoinRequests(slug);
      return response.data;
    },
  });
};
