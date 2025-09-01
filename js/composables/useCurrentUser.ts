import { computed, ref } from 'vue';
import { jwtDecode } from 'jwt-decode';
import { DecodedToken } from '@/models/DecodedToken';
import auth from '@/auth';
import { CurrentUser } from '@/models/CurrentUser';
import { Role } from '@/models/Role';

export default function useCurrentUser(slug: string) {
  const isSignedIn = computed(() => !!currentUser.value.isSignedIn);

  const currentUser = computed((): CurrentUser => {
    const t = decodedToken.value;

    return !t
      ? {
          isSignedIn: false,
          userName: '',
          userDisplayName: '',
          isAdmin: false,
          bunches: [],
        }
      : {
          isSignedIn: true,
          userName: t.unique_name,
          userDisplayName: t.userdisplayname,
          isAdmin: t.isadmin === 'true',
          bunches: t.bunches,
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

  const isManager = computed((): boolean => {
    const bunch = currentUser.value.bunches.find((o) => o.slug === slug);
    return bunch?.role === Role.Manager;
  });

  const playerId = computed((): string => {
    const bunch = currentUser.value.bunches.find((o) => o.slug === slug);
    return bunch?.playerId ?? '';
  });

  return {
    isSignedIn,
    isAdmin,
    isManager,
    playerId,
    currentUser,
  };
}
