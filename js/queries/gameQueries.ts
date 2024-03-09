import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { eventGameListKey, gameListKey } from './queryKeys';

const fetchGames = async (slug: string): Promise<ArchiveCashgame[]> => {
  const response = await api.getGames(slug);
  return response.data.map((o) => ArchiveCashgame.fromResponse(o));
};

const fetchEventGames = async (slug: string, eventId: string): Promise<ArchiveCashgame[]> => {
  const response = await api.getEventGames(slug, eventId);
  return response.data.map((o) => ArchiveCashgame.fromResponse(o));
};

export const useGameListQuery = (slug: string) => {
  return useQuery({ queryKey: gameListKey(slug), queryFn: () => fetchGames(slug) });
};

export const useEventGameListQuery = (slug: string, eventId: string) => {
  return useQuery({ queryKey: eventGameListKey(slug, eventId), queryFn: () => fetchEventGames(slug, eventId) });
};