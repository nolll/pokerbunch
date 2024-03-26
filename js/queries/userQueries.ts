import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';
import auth from '@/auth';
import { currentUserKey, userKey, userListKey } from './queryKeys';
import { fiveMinuteStaleTime } from './staleTimes';
import { AxiosError } from 'axios';

export const useCurrentUserQuery = () => {
  return useQuery({
    queryKey: currentUserKey(),
    queryFn: async (): Promise<User | null> => {
      if (!auth.hasToken()) return null;
      try {
        const response = await api.getCurrentUser();
        return response.data;
      } catch (error: any) {
        if (error.response.status === 401 || error.response.status === 404) {
          return null;
        }
        throw error;
      }
    },
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
