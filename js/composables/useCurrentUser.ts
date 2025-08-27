import { computed, ref } from 'vue';
import { useCurrentUserQuery } from '@/queries/userQueries';
import { jwtDecode } from 'jwt-decode';
import { User } from '@/models/User';
import roles from '@/roles';
import { DecodedToken } from '@/models/DecodedToken';
import auth from '@/auth';
import { CurrentUser } from '@/models/CurrentUser';

export default function useCurrentUser() {
  const currentUserQuery = useCurrentUserQuery();

  const isSignedIn = computed(() => !!currentUser.value);

  const isSignedIn2 = computed(() => !!currentUser2.value.isSignedIn);

  const currentUser = computed((): User | null => {
    return currentUserQuery.data.value!;
  });

  const currentUser2 = computed((): CurrentUser => {
    var t = decodedToken.value;

    return !t
      ? {
          isSignedIn: false,
          userName: '',
          userDisplayName: '',
          isAdmin: false,
        }
      : {
          isSignedIn: true,
          userName: t.unique_name,
          userDisplayName: t.userdisplayname,
          isAdmin: t.isadmin === 'true',
        };
  });

  const decodedToken = computed((): DecodedToken | null => {
    const token = auth.getToken();
    if (!token) return null;

    return jwtDecode<DecodedToken>(token);
  });

  const currentUserReady = computed((): boolean => {
    return !currentUserQuery.isPending.value;
  });

  const isAdmin = computed((): boolean => {
    return currentUser.value?.role === roles.admin;
  });

  const isAdmin2 = computed((): boolean => {
    return currentUser2.value.isAdmin;
  });

  return {
    isSignedIn,
    isSignedIn2,
    isAdmin,
    isAdmin2,
    currentUser,
    currentUser2,
    currentUserReady,
  };
}
