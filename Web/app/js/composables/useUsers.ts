import { computed, ref, watch } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import { UserStoreMutations } from '@/store/helpers/UserStoreHelpers';
import { User } from '@/models/User';
import urls from '@/urls';
import roles from '@/roles';
import auth from '@/auth';
import api from '@/api';

export default function useUsers() {
  const store = useStore();
  const router = useRouter();

  const isUserRequired = ref(true);

  const userReady = computed((): boolean => {
    return store.state.user._userReady;
  });

  const isSignedIn = computed((): boolean => {
    return store.state.user._isSignedIn;
  });

  const usersReady = computed((): boolean => {
    return store.state.user._usersReady;
  });

  const userName = computed((): string => {
    return store.state.user._userName;
  });

  const displayName = computed((): string => {
    return store.state.user._displayName;
  });

  const isAdmin = computed((): boolean => {
    return store.state.user._role === roles.admin;
  });

  const users = computed((): User[] => {
    return store.state.user._users;
  });

  const requireUser = async () => {
    isUserRequired.value = true;
    await loadCurrentUser();
  };

  const loadCurrentUser = async () => {
    isUserRequired.value = false;
    if (!store.state.user._userInitialized) {
      const isLoggedIn = auth.isLoggedIn();
      store.commit(UserStoreMutations.SetUserInitialized);
      store.commit(UserStoreMutations.SetIsSignedIn, isLoggedIn);
      if (isLoggedIn) {
        try {
          const response = await api.getCurrentUser();
          store.commit(UserStoreMutations.SetUser, response.data);
        } catch (err) {
          store.commit(UserStoreMutations.SetUserError);
        }
      } else {
        store.commit(UserStoreMutations.SetUserError);
      }
    }
  };

  const loadUsers = async () => {
    try {
      const response = await api.getUsers();
      store.commit(UserStoreMutations.SetUsers, response.data);
    } catch (error) {
      store.commit(UserStoreMutations.SetUsersError);
    }
    store.commit(UserStoreMutations.SetUsersReady, true);
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
