import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { BunchResponse } from '@/response/BunchResponse';

const fetchBunches = async (): Promise<BunchResponse[]> => {
  const response = await api.getBunches();
  return response.data;
};

export const useBunchListQuery = () => {
  return useQuery({ queryKey: [`bunches`], queryFn: () => fetchBunches() });
};
