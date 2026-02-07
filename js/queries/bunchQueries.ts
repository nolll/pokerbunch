import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { BunchResponse } from '@/response/BunchResponse';
import { bunchKey, bunchListKey, userBunchListKey } from './queryKeys';
import { fiveMinuteStaleTime } from './staleTimes';

export const useBunchListQuery = () => {
  return useQuery({
    queryKey: bunchListKey(),
    queryFn: async (): Promise<BunchResponse[]> => {
      const response = await api.getBunches();
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};

export const useUserBunchListQuery = (isSignedIn: boolean) => {
  return useQuery({
    queryKey: userBunchListKey(isSignedIn),
    queryFn: async (): Promise<BunchResponse[]> => {
      if (!isSignedIn) return [];
      const response = await api.getUserBunches();
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};

export const useBunchQuery = (slug: string) => {
  return useQuery({
    queryKey: bunchKey(slug),
    queryFn: async (): Promise<BunchResponse | null> => {
      if (!slug) return null;
      const response = await api.getBunch(slug);
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};
