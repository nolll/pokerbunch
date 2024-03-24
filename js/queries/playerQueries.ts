import { Player } from '@/models/Player';
import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { mapPlayer } from '@/mappers/responseMappers';
import { playerListKey } from './queryKeys';
import { fiveMinuteStaleTime } from './staleTimes';

export const usePlayerListQuery = (slug: string) => {
  return useQuery({
    queryKey: playerListKey(slug),
    queryFn: async (): Promise<Player[]> => {
      const response = await api.getPlayers(slug);
      return response.data.map((o) => mapPlayer(o));
    },
    staleTime: fiveMinuteStaleTime,
  });
};
