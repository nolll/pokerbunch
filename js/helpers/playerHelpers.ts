import { Player } from '@/models/Player';

export const getPlayer = (players: Player[], id: string): Player | null => {
  if (!players) return null;
  let i;
  for (i = 0; i < players.length; i++) {
    if (players[i].id === id) {
      return players[i];
    }
  }
  return null;
};
