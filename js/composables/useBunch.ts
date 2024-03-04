import { computed } from 'vue';
import { useBunchQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';
import { Localization } from '@/models/Localization';

export default function useBunch(slug: string) {
  const bunchQuery = useBunchQuery(slug);

  const bunch = computed((): BunchResponse => {
    return bunchQuery.data.value!;
  });

  const localization = computed((): Localization => {
    return {
      timezone: bunch.value.timezone,
      currencyFormat: bunch.value.currencyFormat,
      thousandSeparator: bunch.value.thousandSeparator,
    };
  });

  const bunchReady = computed((): boolean => {
    return !bunchQuery.isPending.value;
  });

  return {
    bunchReady,
    bunch,
    localization,
  };
}
