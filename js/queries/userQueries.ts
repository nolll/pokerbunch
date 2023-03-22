import api from '@/api';
import { useQuery } from 'vue-query';

export const currentUserQueryKey = () => ['currentUser'];

export const useCurrentUserQuery = (isSignedIn: boolean) => {
  return useQuery(currentUserQueryKey(), () => api.getCurrentUser(), {
    select: (response) => response.data,
    enabled: isSignedIn,
  });
};
