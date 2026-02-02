import api from '@/api';
import { useQuery } from '@tanstack/vue-query';
import { User } from '@/models/User';
import { userKey, userListKey } from './queryKeys';
import { fiveMinuteStaleTime } from './staleTimes';

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

export const useUserQuery = (userName: string) => {
  return useQuery({
    queryKey: userKey(userName),
    queryFn: async (): Promise<User> => {
      const response = await api.getUser(userName);
      return response.data;
    },
    staleTime: fiveMinuteStaleTime,
  });
};
