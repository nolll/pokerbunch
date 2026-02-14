import { Timezone } from '@/models/Timezone';

export default function useTimezones() {
  const fallbackTimezone = '';

  const getTimezones = (): Timezone[] => {
    const ids = (Intl as any).supportedValuesOf('timeZone') as string[];
    return ids.map((id: string) => ({
      id: id,
      name: id,
    }));
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
