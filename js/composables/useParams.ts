import { computed } from 'vue';
import { useRoute } from 'vue-router';

export default function useParams() {
  const route = useRoute();

  return {
    slug: computed(() => route.params.slug as string),
    playerId: computed(() => route.params.id as string),
    cashgameId: computed(() => route.params.id as string),
    locationId: computed(() => route.params.id as string),
    eventId: computed(() => route.params.id as string),
    userName: computed(() => route.params.userName as string),
    code: computed(() => route.params.code as string),
    year: computed(() => {
      var s = route.params.year as string | undefined;
      if (!s || s === '') return undefined;
      return parseInt(s);
    }),
  };
}
