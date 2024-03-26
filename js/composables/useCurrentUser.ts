import { computed, ref } from 'vue';
import { useCurrentUserQuery } from '@/queries/userQueries';
import { User } from '@/models/User';
import roles from '@/roles';

export default function useCurrentUser() {
  const currentUserQuery = useCurrentUserQuery();

  const isSignedIn = computed(() => !!currentUser.value);

  const currentUser = computed((): User | null => {
    return currentUserQuery.data.value!;
  });

  const currentUserReady = computed((): boolean => {
    return !currentUserQuery.isPending.value;
  });

  const isAdmin = computed((): boolean => {
    return currentUser.value?.role === roles.admin;
  });

  return {
    isSignedIn,
    isAdmin,
    currentUser,
    currentUserReady,
  };
}
