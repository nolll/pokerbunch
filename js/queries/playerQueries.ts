import api from '@/api';
import { useQuery, useMutation } from 'vue-query';

export const playersQueryKey = (slug: string) => ['players', slug];

export const usePlayersQuery = (slug: string) => {
  return useQuery(playersQueryKey(slug), () => api.getPlayers(slug), {
    select: (response) => response.data,
  });
};
