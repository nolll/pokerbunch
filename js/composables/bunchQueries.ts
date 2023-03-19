import api from '@/api';
import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
import { useQuery, useMutation } from 'vue-query';

const bunchQueryKeyStr = 'bunches';
const userBunchesQueryKeyStr = 'userbunches';

export function bunchQueryKey(slug: string) {
  return [bunchQueryKeyStr, slug];
}

export function userBunchesQueryKey() {
  return [userBunchesQueryKeyStr];
}

// export function locationsQueryKey(slug: string) {
//   return [queryKey, slug];
// }

export function useBunchQuery(slug: string) {
  return useQuery(bunchQueryKey(slug), () => api.getBunch(slug), {
    select: (response) => response.data,
    enabled: !!slug,
  });
}

export function useUserBunchesQuery() {
  return useQuery(userBunchesQueryKey(), () => api.getUserBunches(), {
    select: (response) => response.data,
  });
}

// export function useLocationsQuery(slug: string) {
//   return useQuery(locationsQueryKey(slug), () => api.getLocations(slug), {
//     select: (response) => response.data,
//   });
// }

export function useUpdateBunchMutation(id: string, onSuccess: () => void | Promise<unknown>) {
  return useMutation({
    mutationFn: (bunch: ApiParamsUpdateBunch) => api.updateBunch(id, bunch),
    onSuccess: () => onSuccess(),
  });
}
