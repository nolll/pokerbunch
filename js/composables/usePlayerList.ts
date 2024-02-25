import { computed } from 'vue';
import { Player } from '@/models/Player';
import { usePlayerListQuery } from '@/queries/playerQueries';

export default function usePlayerList(slug: string) {
  const playerListQuery = usePlayerListQuery(slug);

  const players = computed((): Player[] => {
    return playerListQuery.data.value ?? [];
  });

  const playersReady = computed((): boolean => {
    return !playerListQuery.isPending.value;
  });

  const getPlayer = (id: string): Player => {
    const p = players.value.find((o) => o.id === id);
    if (!p) throw new Error(`player not found: ${id}`);
    return p;
  };

  return {
    playersReady: playersReady,
    players: players,
    getPlayer: getPlayer,
  };
}
