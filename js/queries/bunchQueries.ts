import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { BunchResponse } from '@/response/BunchResponse';
import auth from '@/auth';
import { bunchKey, bunchListKey, userBunchListKey } from './queryKeys';

const fetchBunches = async (): Promise<BunchResponse[]> => {
  const response = await api.getBunches();
  return response.data;
};

const fetchUserBunches = async (): Promise<BunchResponse[]> => {
  if (!auth.isLoggedIn()) return [];
  const response = await api.getUserBunches();
  return response.data;
};

const fetchBunch = async (slug: string): Promise<BunchResponse | null> => {
  if (!slug) return null;
  const response = await api.getBunch(slug);
  return response.data;
};

export const useBunchListQuery = () => {
  return useQuery({ queryKey: bunchListKey(), queryFn: () => fetchBunches() });
};

export const useUserBunchListQuery = () => {
  return useQuery({ queryKey: userBunchListKey(), queryFn: () => fetchUserBunches() });
};

export const useBunchQuery = (slug: string) => {
  return useQuery({ queryKey: bunchKey(slug), queryFn: () => fetchBunch(slug) });
};
