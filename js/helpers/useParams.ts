import { computed } from 'vue';
import { useRoute } from 'vue-router';

export default function useParams() {
  const route = useRoute();

  return {
    slug: computed(() => route.params.slug as string),
    id: computed(() => route.params.id as string),
    year: computed(() => {
      const s = route.params.year as string;
      if (!s || s === '') return undefined;
      return parseInt(s);
    }),
  };
}
