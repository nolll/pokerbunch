import { Timezone } from '@/models/Timezone';

export default function useTimezones() {
  const fallbackTimezone = '';

  const getTimezones = (): Timezone[] => {
    const ids = Intl.supportedValuesOf('timeZone');
    return ids.map((id) => {
      return {
        id: id,
        name: id,
      };
    });
  };

  const getDefaultTimezone = () => {
    try {
      const clientTimezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
      return !!clientTimezone ? clientTimezone : fallbackTimezone;
    } catch {
      return fallbackTimezone;
    }
  };

  return {
    getTimezones,
    getDefaultTimezone,
  };
}
