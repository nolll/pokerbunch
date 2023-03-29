import api from '@/api';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { useQuery } from 'vue-query';

export const gameArchiveQueryKey = (slug: string) => ['gameArchive', slug];
export const eventGamesQueryKey = (slug: string, id: string) => ['eventGames', slug, id];

export const useGameArchiveQuery = (slug: string) => {
  return useQuery(gameArchiveQueryKey(slug), () => api.getGames(slug), {
    select: (response) => response.data.map((o) => ArchiveCashgame.fromResponse(o)),
  });
};

export const useEventGamesQuery = (slug: string, id: string) => {
  return useQuery(eventGamesQueryKey(slug, id), () => api.getEventGames(slug, id), {
    select: (response) => response.data.map((o) => ArchiveCashgame.fromResponse(o)),
  });
};
