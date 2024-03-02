import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { locationListKey } from './queryKeys';
import { LocationResponse } from '@/response/LocationResponse';

const fetchLocations = async (slug: string): Promise<LocationResponse[]> => {
  const response = await api.getLocations(slug);
  return response.data;
};

export const useLocationListQuery = (slug: string) => {
  return useQuery({ queryKey: locationListKey(slug), queryFn: () => fetchLocations(slug) });
};
