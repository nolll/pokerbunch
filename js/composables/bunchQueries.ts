import api from '@/api';
import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
import { useQuery, useMutation } from 'vue-query';

const queryKey = 'bunches';

export function bunchQueryKey(slug: string) {
  return [queryKey, slug];
}

// export function locationsQueryKey(slug: string) {
//   return [queryKey, slug];
// }

export function useBunchQuery(slug: string) {
  return useQuery(bunchQueryKey(slug), () => api.getBunch(slug), {
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
