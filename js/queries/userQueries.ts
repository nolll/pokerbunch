import api from '@/api';
import { useQuery } from 'vue-query';

export function currentUserQueryKey() {
  return ['currentUser'];
}

export function useCurrentUserQuery() {
  return useQuery(currentUserQueryKey(), () => api.getCurrentUser(), {
    select: (response) => response.data,
  });
}
