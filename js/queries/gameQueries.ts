import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { gameListKey } from './queryKeys';

const fetchGames = async (slug: string): Promise<ArchiveCashgame[]> => {
  const response = await api.getGames(slug);
  return response.data.map((o) => ArchiveCashgame.fromResponse(o));
};

export const useGameListQuery = (slug: string) => {
  return useQuery({ queryKey: gameListKey(slug), queryFn: () => fetchGames(slug) });
};
