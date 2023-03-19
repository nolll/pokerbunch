import api from '@/api';
import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
import { useQuery, useMutation } from 'vue-query';

export function bunchQueryKey(slug: string) {
  return ['bunch', slug];
}

export function bunchesQueryKey() {
  return ['bunches'];
}

export function userBunchesQueryKey() {
  return ['userbunches'];
}

export function useBunchQuery(slug: string) {
  return useQuery(bunchQueryKey(slug), () => api.getBunch(slug), {
    select: (response) => response.data,
    enabled: !!slug,
  });
}

export function useBunchesQuery() {
  return useQuery(bunchesQueryKey(), () => api.getBunches(), {
    select: (response) => response.data,
  });
}

export function useUserBunchesQuery() {
  return useQuery(userBunchesQueryKey(), () => api.getUserBunches(), {
    select: (response) => response.data,
  });
}

export function useUpdateBunchMutation(id: string, onSuccess: () => void | Promise<unknown>) {
  return useMutation({
    mutationFn: (bunch: ApiParamsUpdateBunch) => api.updateBunch(id, bunch),
    onSuccess: () => onSuccess(),
  });
}
