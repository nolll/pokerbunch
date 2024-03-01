import { computed } from 'vue';
import { useUserListQuery } from '@/queries/userQueries';
import { User } from '@/models/User';

export default function useUserList() {
  const userListQuery = useUserListQuery();

  const users = computed((): User[] => {
    return userListQuery.data.value ?? [];
  });

  const usersReady = computed((): boolean => {
    return !userListQuery.isPending.value;
  });

  return {
    users,
    usersReady,
  };
}
