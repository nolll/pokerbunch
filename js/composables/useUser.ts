import { computed } from 'vue';
import { useUserQuery } from '@/queries/userQueries';
import { User } from '@/models/User';

export default function useUser(userName: string) {
  const userQuery = useUserQuery(userName);

  return {
    user: computed((): User => userQuery.data.value!),
    userReady: computed((): boolean => !userQuery.isPending.value),
  };
}
