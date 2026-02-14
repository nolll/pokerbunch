import { computed } from 'vue';
import { useUserListQuery } from '@/queries/userQueries';
import { User } from '@/models/User';

export default function useUserList() {
  const userListQuery = useUserListQuery();

  return {
    users: computed((): User[] => userListQuery.data.value ?? []),
    usersReady: computed((): boolean => !userListQuery.isPending.value),
  };
}
