import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';

const fetchCurrentUser = async (): Promise<User> => {
  const response = await api.getCurrentUser();
  return response.data;
};

export const useCurrentUserQuery = () => {
  return useQuery({ queryKey: ['current-user'], queryFn: () => fetchCurrentUser() });
};
