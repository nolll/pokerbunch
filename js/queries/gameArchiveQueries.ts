import api from '@/api';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { useQuery } from 'vue-query';

export const gameArchiveQueryKey = () => ['gameArchive'];

export const useGameArchiveQuery = (slug: string) => {
  return useQuery(gameArchiveQueryKey(), () => api.getGames(slug), {
    select: (response) => response.data.map((o) => ArchiveCashgame.fromResponse(o)),
  });
};
