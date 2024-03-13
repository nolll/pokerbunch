import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';
import auth from '@/auth';
import { currentUserKey, userKey, userListKey } from './queryKeys';

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
  return useQuery({ queryKey: currentUserKey(), queryFn: () => fetchCurrentUser() });
};

export const useUserListQuery = () => {
  return useQuery({ queryKey: userListKey(), queryFn: () => fetchUsers() });
};

export const useUserQuery = (userName: string, isEnabled: boolean) => {
  return useQuery({ queryKey: userKey(userName), queryFn: () => fetchUser(userName), enabled: isEnabled });
};
