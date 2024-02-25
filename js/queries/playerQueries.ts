import { Player } from '@/models/Player';
import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { mapPlayer } from '@/mappers/responseMappers';

const fetchPlayers = async (slug: string): Promise<Player[]> => {
  const response = await api.getPlayers(slug);
  return response.data.map((o) => mapPlayer(o));
};

export const usePlayerListQuery = (slug: string) => {
  return useQuery({ queryKey: [`players-${slug}`], queryFn: () => fetchPlayers(slug) });
};
