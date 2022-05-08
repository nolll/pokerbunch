import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { PlayerStoreMutations } from '@/store/helpers/PlayerStoreHelpers';
import { Player } from '@/models/Player';
import api from '@/api';
import { PlayerResponse } from '@/response/PlayerResponse';

export default function usePlayers() {
  const store = useStore();
  const route = useRoute();

  const playersReady = computed((): boolean => {
    return store.state.players._playersReady;
  });

  const players = computed((): Player[] => {
    return store.state.player._players;
  });

  const getPlayer = (id: string): Player => {
    if (!store.state.player._players) throw new Error('players not loaded');
    let i;
    for (i = 0; i < store.state.player._players.length; i++) {
      if (store.state.player._players[i].id === id) {
        return store.state.player._players[i];
      }
    }
    throw new Error(`player not found: ${id}`);
  };

  const loadPlayers = async () => {
    const slug = route.params.slug as string;
    if (slug !== store.state.state.player._slug) {
      store.commit(PlayerStoreMutations.SetSlug, slug);
      const response = await api.getPlayers(slug);
      const players = response.data.map((o) => mapPlayer(o));
      store.commit(PlayerStoreMutations.SetPlayersData, players);
    }
  };

  const addPlayer = async (name: string) => {
    const slug = route.params.slug as string;
    if (store.state.player._playersReady) {
      const response = await api.addPlayer(slug, { name: name });
      const player = mapPlayer(response.data);
      store.commit(PlayerStoreMutations.AddPlayer, player);
    }
  };

  const deletePlayer = async (player: Player) => {
    if (store.state.player._playersReady) {
      store.commit(PlayerStoreMutations.DeletePlayer, player);
      await api.deletePlayer(player.id);
    }
  };

  const mapPlayer = (response: PlayerResponse): Player => {
    return {
      id: response.id.toString(),
      name: response.name,
      color: response.color,
      userId: response.userId,
      userName: response.userName,
    };
  };

  return {
    playersReady,
    players,
    getPlayer,
    loadPlayers,
    addPlayer,
    deletePlayer,
  };
}
