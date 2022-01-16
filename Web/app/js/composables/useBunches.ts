import { BunchStoreActions, BunchStoreGetters } from '@/store/helpers/BunchStoreHelpers';
import { computed } from 'vue';
import { useStore } from 'vuex';
import { useRoute } from 'vue-router';
import { BunchResponse } from '@/response/BunchResponse';

export default function useBunches() {
  const store = useStore();
  const route = useRoute();

  const bunchReady = computed((): boolean => {
    return store.getters[BunchStoreGetters.BunchReady];
  });

  const defaultBuyin = computed((): number => {
    return store.getters[BunchStoreGetters.DefaultBuyin];
  });

  const isManager = computed((): boolean => {
    return store.getters[BunchStoreGetters.IsManager];
  });

  const bunch = computed((): BunchResponse => {
    return store.getters[BunchStoreGetters.Bunch];
  });

  const slug = computed((): string => {
    return store.getters[BunchStoreGetters.Slug];
  });

  const bunchName = computed((): string => {
    return store.getters[BunchStoreGetters.Name];
  });

  const playerId = computed((): string => {
    return store.getters[BunchStoreGetters.PlayerId];
  });

  const userBunches = computed((): BunchResponse[] => {
    return store.getters[BunchStoreGetters.UserBunches];
  });

  const userBunchesReady = computed((): boolean => {
    return store.getters[BunchStoreGetters.UserBunchesReady];
  });

  const bunches = computed((): BunchResponse[] => {
    return store.getters[BunchStoreGetters.Bunches];
  });

  const bunchesReady = computed((): boolean => {
    return store.getters[BunchStoreGetters.BunchesReady];
  });

  const description = computed((): string => {
    return store.getters[BunchStoreGetters.Description];
  });

  const houseRules = computed((): string => {
    return store.getters[BunchStoreGetters.HouseRules];
  });

  const currencyFormat = computed((): string => {
    return store.getters[BunchStoreGetters.CurrencyFormat];
  });

  const thousandSeparator = computed((): string => {
    return store.getters[BunchStoreGetters.ThousandSeparator];
  });

  const loadBunch = () => {
    store.dispatch(BunchStoreActions.LoadBunch, { slug: route.params.slug });
  };

  const refreshBunch = () => {
    store.dispatch(BunchStoreActions.LoadBunch, { slug: route.params.slug, forceLoad: true });
  };

  const loadUserBunches = () => {
    store.dispatch(BunchStoreActions.LoadUserBunches);
  };

  const loadBunches = () => {
    store.dispatch(BunchStoreActions.LoadBunches);
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
    bunchesReady,
    description,
    houseRules,
    currencyFormat,
    thousandSeparator,
    loadBunch,
    refreshBunch,
    loadUserBunches,
    loadBunches,
  };
}
