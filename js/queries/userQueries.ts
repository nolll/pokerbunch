import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';
import auth from '@/auth';

const fetchCurrentUser = async (): Promise<User | null> => {
  if (!auth.isLoggedIn()) return null;
  const response = await api.getCurrentUser();
  return response.data;
};

export const useCurrentUserQuery = () => {
  return useQuery({ queryKey: ['current-user'], queryFn: () => fetchCurrentUser() });
};
