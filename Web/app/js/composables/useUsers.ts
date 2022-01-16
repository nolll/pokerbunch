import { computed, ref, watch } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { UserStoreActions, UserStoreGetters } from '@/store/helpers/UserStoreHelpers';
import { User } from '@/models/User';
import urls from '@/urls';

export default function useLocations() {
  const store = useStore();
  const router = useRouter();

  const isUserRequired = ref(true);

  const userReady = computed((): boolean => {
    return store.getters[UserStoreGetters.UserReady];
  });

  const isSignedIn = computed((): boolean => {
    return store.getters[UserStoreGetters.IsSignedIn];
  });

  const usersReady = computed((): boolean => {
    return store.getters[UserStoreGetters.UsersReady];
  });

  const userName = computed((): string => {
    return store.getters[UserStoreGetters.UserName];
  });

  const displayName = computed((): string => {
    return store.getters[UserStoreGetters.DisplayName];
  });

  const isAdmin = computed((): boolean => {
    return store.getters[UserStoreGetters.IsAdmin];
  });

  const users = computed((): User[] => {
    return store.getters[UserStoreGetters.Users];
  });

  const requireUser = () => {
    isUserRequired.value = true;
    store.dispatch(UserStoreActions.LoadCurrentUser);
  };

  const loadCurrentUser = () => {
    isUserRequired.value = false;
    store.dispatch(UserStoreActions.LoadCurrentUser);
  };

  const loadUsers = () => {
    store.dispatch(UserStoreActions.LoadUsers);
  };

  watch(userReady, (isUserReady: boolean) => {
    if (isUserReady && isUserRequired.value && !isSignedIn.value) {
      router.push(urls.auth.login);
    }
  });

  return {
    userReady,
    isSignedIn,
    usersReady,
    userName,
    displayName,
    isAdmin,
    users,
    requireUser,
    loadCurrentUser,
    loadUsers,
  };
}
