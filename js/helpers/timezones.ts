import { Timezone } from '@/models/Timezone';

const fallbackTimezone = '';

export const getTimezones = (): Timezone[] => {
  const ids = (Intl as any).supportedValuesOf('timeZone') as string[];
  return ids.map((id: string) => {
    return {
      id: id,
      name: id,
    };
  });
};

export const getDefaultTimezone = () => {
  try {
    const clientTimezone = Intl.DateTimeFormat().resolvedOptions().timeZone;
    return !!clientTimezone ? clientTimezone : fallbackTimezone;
  } catch {
    return fallbackTimezone;
  }
};
