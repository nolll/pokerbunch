import { BunchStoreMutations } from '@/store/helpers/BunchStoreHelpers';
import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { BunchResponse } from '@/response/BunchResponse';
import roles from '@/roles';
import api from '@/api';

export default function useBunches() {
  const store = useStore();
  const route = useRoute();

  const bunchReady = computed((): boolean => {
    return store.state.bunch._bunchReady;
  });

  const defaultBuyin = computed((): number => {
    return store.state.bunch._defaultBuyin;
  });

  const isManager = computed((): boolean => {
    return store.state.bunch._role === roles.manager || store.state.bunch._role === roles.admin;
  });

  const bunch = computed((): BunchResponse => {
    return store.state.bunch._bunch;
  });

  const slug = computed((): string => {
    return store.state.bunch._slug;
  });

  const bunchName = computed((): string => {
    return store.state.bunch._name;
  });

  const playerId = computed((): string => {
    return store.state.bunch._playerId;
  });

  const userBunches = computed((): BunchResponse[] => {
    return store.state.bunch._userBunches;
  });

  const userBunchesReady = computed((): boolean => {
    return store.state.bunch._userBunchesReady;
  });

  const bunches = computed((): BunchResponse[] => {
    return store.state.bunch._bunches;
  });

  const description = computed((): string | null => {
    const d = store.state.bunch._description;
    return d && d.length > 0 ? d : null;
  });

  const houseRules = computed((): string | null => {
    const r = store.state.bunch._houseRules;
    return r && r.length > 0 ? r : null;
  });

  const currencyFormat = computed((): string => {
    return store.state.bunch._currencyFormat;
  });

  const thousandSeparator = computed((): string => {
    return store.state.bunch._thousandSeparator;
  });

  const loadBunch = async (forceLoad: boolean | undefined = undefined) => {
    const slug = route.params.slug as string;
    if (forceLoad || slug !== store.state.bunch._slug) {
      store.commit(BunchStoreMutations.SetBunchReady, false);
      const response = await api.getBunch(slug);
      store.commit(BunchStoreMutations.SetBunchData, response.data);
      store.commit(BunchStoreMutations.SetBunchReady, true);
    }
  };

  const refreshBunch = async () => {
    await loadBunch(true);
  };

  return {
    bunchReady,
    defaultBuyin,
    isManager,
    bunch,
    slug,
    bunchName,
    playerId,
    userBunches,
    userBunchesReady,
    bunches,
    description,
    houseRules,
    currencyFormat,
    thousandSeparator,
    loadBunch,
    refreshBunch,
  };
}
