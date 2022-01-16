import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { PlayerStoreActions, PlayerStoreGetters } from '@/store/helpers/PlayerStoreHelpers';
import { Player } from '@/models/Player';

export default function useLocations() {
  const store = useStore();
  const route = useRoute();

  const playersReady = computed((): boolean => {
    return store.getters[PlayerStoreGetters.PlayersReady];
  });

  const players = computed((): Player[] => {
    return store.getters[PlayerStoreGetters.Players];
  });

  const getPlayer = (id: string): Player => {
    return store.getters[PlayerStoreGetters.GetPlayer](id);
  };

  const loadPlayers = () => {
    store.dispatch(PlayerStoreActions.LoadPlayers, { slug: route.params.slug });
  };

  const addPlayer = (name: string) => {
    store.dispatch(PlayerStoreActions.AddPlayer, { bunchId: route.params.slug, name });
  };

  const deletePlayer = (player: Player) => {
    store.dispatch(PlayerStoreActions.DeletePlayer, { player });
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
