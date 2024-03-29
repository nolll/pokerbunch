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

  const cashgameId = computed(() => {
    return route.params.id as string;
  });

  const locationId = computed(() => {
    return route.params.id as string;
  });

  const eventId = computed(() => {
    return route.params.id as string;
  });

  const userName = computed(() => {
    return route.params.userName as string;
  });

  const code = computed(() => {
    return route.params.code as string;
  });

  const year = computed(() => {
    var s = route.params.year as string | undefined;
    if (!s || s === '') return undefined;
    return parseInt(s);
  });

  return {
    slug,
    playerId,
    cashgameId,
    locationId,
    eventId,
    userName,
    code,
    year,
  };
}
