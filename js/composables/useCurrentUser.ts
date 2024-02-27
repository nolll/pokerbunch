import { computed, ref } from 'vue';
import auth from '@/auth';
import { useCurrentUserQuery } from '@/queries/userQueries';
import { User } from '@/models/User';
import roles from '@/roles';

export default function useUserBunchList() {
  const currentUserQuery = useCurrentUserQuery();
  const isSignedIn = ref(auth.isLoggedIn());

  const currentUser = computed((): User => {
    return currentUserQuery.data.value!;
  });

  const currentUserReady = computed((): boolean => {
    return !currentUserQuery.isPending.value;
  });

  const isAdmin = computed((): boolean => {
    return currentUser.value.role === roles.admin;
  });

  return {
    isSignedIn,
    isAdmin,
    currentUser,
    currentUserReady,
  };
}
