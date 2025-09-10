import { computed } from 'vue';
import { useRoute } from 'vue-router';

export default function useParams() {
  const route = useRoute();

  const slug = computed(() => route.params.slug as string);
  const playerId = computed(() => route.params.id as string);
  const cashgameId = computed(() => route.params.id as string);
  const locationId = computed(() => route.params.id as string);
  const eventId = computed(() => route.params.id as string);
  const userName = computed(() => route.params.userName as string);
  const code = computed(() => route.params.code as string);

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
