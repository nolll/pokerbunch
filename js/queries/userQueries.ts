import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';
import auth from '@/auth';
import { currentUserKey, userKey, userListKey } from './queryKeys';
import { fiveMinuteStaleTime } from './staleTimes';

export const useCurrentUserQuery = () => {
  return useQuery({
    queryKey: currentUserKey(),
    queryFn: async (): Promise<User | null> => {
      if (!auth.isLoggedIn()) return null;
      const response = await api.getCurrentUser();
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};

export const useUserListQuery = () => {
  return useQuery({
    queryKey: userListKey(),
    queryFn: async (): Promise<User[]> => {
      const response = await api.getUsers();
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};

export const useUserQuery = (userName: string, isEnabled: boolean) => {
  return useQuery({
    queryKey: userKey(userName),
    queryFn: async (): Promise<User> => {
      const response = await api.getUser(userName);
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
    enabled: isEnabled,
  });
};
