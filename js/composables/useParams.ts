import { computed } from 'vue';
import { useRoute } from 'vue-router';

export default function useParams() {
  const route = useRoute();

  return {
    slug: computed(() => {
      return route.params.slug as string;
    }),
  };
}
