import api from '@/api';
import { useQuery, useMutation } from 'vue-query';

export const currentGamesQueryKey = (slug: string) => ['currentgames', slug];

export const useCurrentGamesQuery = (slug: string) => {
  return useQuery(currentGamesQueryKey(slug), () => api.getCurrentGames(slug), {
    select: (response) => response.data,
  });
};
