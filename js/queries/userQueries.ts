import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';
import auth from '@/auth';

const fetchCurrentUser = async (): Promise<User | null> => {
  if (!auth.isLoggedIn()) return null;
  const response = await api.getCurrentUser();
  return response.data;
};

const fetchUsers = async (): Promise<User[]> => {
  const response = await api.getUsers();
  return response.data;
};

const fetchUser = async (userName: string): Promise<User> => {
  const response = await api.getUser(userName);
  return response.data;
};

export const useCurrentUserQuery = () => {
  return useQuery({ queryKey: ['current-user'], queryFn: () => fetchCurrentUser() });
};

export const useUserListQuery = () => {
  return useQuery({ queryKey: ['users'], queryFn: () => fetchUsers() });
};

export const useUserQuery = (userName: string) => {
  return useQuery({ queryKey: ['user', userName], queryFn: () => fetchUser(userName) });
};
