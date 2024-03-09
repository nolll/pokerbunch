import { computed } from 'vue';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { useEventGameListQuery } from '@/queries/gameQueries';

export default function useEventGameList(slug: string, eventId: string) {
  const { data, isPending } = useEventGameListQuery(slug, eventId);

  const eventGames = computed((): ArchiveCashgame[] => {
    return data.value ?? [];
  });

  const eventGamesReady = computed((): boolean => {
    return !isPending.value;
  });

  return {
    eventGames,
    eventGamesReady,
  };
}
