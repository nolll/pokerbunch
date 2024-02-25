import { computed } from 'vue';
import { useRoute } from 'vue-router';

export default function useParams() {
  const route = useRoute();

  const slug = computed(() => {
    return route.params.slug as string;
  });

  const playerId = computed(() => {
    return route.params.id as string;
  });

  return {
    slug,
    playerId,
  };
}
