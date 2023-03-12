import api from '@/api';
import { useMutation } from 'vue-query';

export function useAddLocationMutation(slug: string) {
  return useMutation((location: object) => api.addLocation(slug, location));
}
