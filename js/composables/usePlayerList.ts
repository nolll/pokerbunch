import { computed } from 'vue';
import { Player } from '@/models/Player';
import { usePlayerListQuery } from '@/queries/playerQueries';

export default function usePlayerList(slug: string) {
  const playerListQuery = usePlayerListQuery(slug);

  const players = computed((): Player[] => playerListQuery.data.value ?? []);
  const playersReady = computed((): boolean => !playerListQuery.isPending.value);

  const getPlayer = (id: string): Player | undefined => {
    if (!playersReady.value) return undefined;
    return players.value.find((o) => o.id === id);
  };

  const tryGetPlayer = (id: string): Player | undefined => {
    return players.value.find((o) => o.id === id);
  };

  return {
    playersReady,
    players,
    getPlayer,
    tryGetPlayer,
  };
}
