import { computed, ref } from 'vue';
import { jwtDecode } from 'jwt-decode';
import { DecodedToken } from '@/models/DecodedToken';
import auth from '@/auth';
import { CurrentUser } from '@/models/CurrentUser';

export default function useCurrentUser() {
  const isSignedIn = computed(() => !!currentUser.value.isSignedIn);

  const currentUser = computed((): CurrentUser => {
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

  const isAdmin = computed((): boolean => {
    return currentUser.value.isAdmin;
  });

  return {
    isSignedIn,
    isAdmin,
    currentUser,
  };
}
