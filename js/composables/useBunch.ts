import { computed } from 'vue';
import { useBunchQuery } from '@/queries/bunchQueries';
import { BunchResponse } from '@/response/BunchResponse';
import { Localization } from '@/models/Localization';

export default function useBunch(slug: string) {
  const bunchQuery = useBunchQuery(slug);

  return {
    bunchReady: computed((): boolean => !bunchQuery.isPending.value),
    bunch: computed((): BunchResponse => bunchQuery.data.value!),
    localization: computed(
      (): Localization => ({
        timezone: computed((): BunchResponse => bunchQuery.data.value!).value.timezone,
        currencyFormat: computed((): BunchResponse => bunchQuery.data.value!).value.currencyFormat,
        thousandSeparator: computed((): BunchResponse => bunchQuery.data.value!).value.thousandSeparator,
      }),
    ),
  };
}
