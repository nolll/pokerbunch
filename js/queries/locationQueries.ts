import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { locationListKey } from './queryKeys';
import { LocationResponse } from '@/response/LocationResponse';
import { fiveMinuteStaleTime } from './staleTimes';

export const useLocationListQuery = (slug: string) => {
  return useQuery({
    queryKey: locationListKey(slug),
    queryFn: async (): Promise<LocationResponse[]> => {
      const response = await api.getLocations(slug);
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};
