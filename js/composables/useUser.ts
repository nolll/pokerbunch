import { computed } from 'vue';
import { useUserQuery } from '@/queries/userQueries';
import { User } from '@/models/User';

export default function useUser(userName: string, isEnabled?: boolean) {
  const userQuery = useUserQuery(userName, isEnabled ?? true);

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
