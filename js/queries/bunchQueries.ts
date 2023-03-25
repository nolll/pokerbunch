import api from '@/api';
import { useQuery } from 'vue-query';

export const bunchQueryKey = (slug: string) => ['bunch', slug];
export const bunchesQueryKey = () => ['bunches'];
export const userBunchesQueryKey = () => ['userbunches'];

export const useBunchQuery = (slug: string) => {
  return useQuery(bunchQueryKey(slug), () => api.getBunch(slug), {
    select: (response) => response.data,
    enabled: !!slug,
  });
};

export const useBunchesQuery = () => {
  return useQuery(bunchesQueryKey(), () => api.getBunches(), {
    select: (response) => response.data,
  });
};

export const useUserBunchesQuery = (isSignedIn: boolean) => {
  return useQuery(userBunchesQueryKey(), () => api.getUserBunches(), {
    select: (response) => response.data,
    enabled: isSignedIn,
  });
};
