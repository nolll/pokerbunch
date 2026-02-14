import { computed } from 'vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { useEventGameListQuery } from '@/queries/gameQueries';

export default function useEventGameList(slug: string, eventId: string) {
  const { data, isPending } = useEventGameListQuery(slug, eventId);

  return {
    eventGames: computed((): ArchiveCashgame[] => data.value ?? []),
    eventGamesReady: computed((): boolean => !isPending.value),
  };
}
