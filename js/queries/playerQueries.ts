import api from '@/api';
import { Player } from '@/models/Player';
import { PlayerResponse } from '@/response/PlayerResponse';
import { useQuery } from 'vue-query';

export const playersQueryKey = (slug: string) => ['players', slug];

export const usePlayersQuery = (slug: string) => {
  return useQuery(playersQueryKey(slug), () => api.getPlayers(slug), {
    select: (response) => {
      return response.data.map((o) => mapPlayer(o));
    },
  });
};

const mapPlayer = (response: PlayerResponse): Player => {
  return {
    id: response.id,
    name: response.name,
    color: response.color,
    userId: response.userId,
    userName: response.userName,
  };
};
