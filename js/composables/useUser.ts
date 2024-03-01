import { computed } from 'vue';
import { useUserQuery } from '@/queries/userQueries';
import { User } from '@/models/User';

export default function useUser(userName: string) {
  const userQuery = useUserQuery(userName);

  const user = computed((): User => {
    return userQuery.data.value!;
  });

  const userReady = computed((): boolean => {
    return !userQuery.isPending.value;
  });

  return {
    user,
    userReady,
  };
}
