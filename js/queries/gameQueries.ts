import api from '@/api';
import { skipToken, useQuery } from '@tanstack/vue-query';
import { ArchiveCashgame } from '@/models/ArchiveCashgame';
import { currentGameListKey, eventGameListKey, gameKey, gameListKey } from './queryKeys';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';
import { DetailedCashgame } from '@/models/DetailedCashgame';
import { fifteenSecondsRefreshInterval, thirtySecondsRefreshInterval, thirtySecondsStaleTime } from './staleTimes';
import { Ref } from 'vue';

export const useGameListQuery = (slug: string) => {
  return useQuery({
    queryKey: gameListKey(slug),
    queryFn: async (): Promise<ArchiveCashgame[]> => {
      const response = await api.getGames(slug);
      return response.data.map((o) => ArchiveCashgame.fromResponse(o));
    },
    staleTime: thirtySecondsStaleTime,
  });
};

export const useEventGameListQuery = (slug: string, eventId: string) => {
  return useQuery({
    queryKey: eventGameListKey(slug, eventId),
    queryFn: async (): Promise<ArchiveCashgame[]> => {
      const response = await api.getEventGames(slug, eventId);
      return response.data.map((o) => ArchiveCashgame.fromResponse(o));
    },
    staleTime: thirtySecondsStaleTime,
  });
};

export const useCurrentGameListQuery = (slug: string) => {
  return useQuery({
    queryKey: currentGameListKey(slug),
    queryFn: async (): Promise<CurrentGameResponse[]> => {
      const response = await api.getCurrentGames(slug);
      return response.data;
    },
    staleTime: thirtySecondsStaleTime,
    refetchInterval: thirtySecondsRefreshInterval,
  });
};

export const useGameQuery = (id: string, isEnabled: Ref<boolean, boolean>) => {
  return useQuery({
    queryKey: gameKey(id),
    enabled: isEnabled,
    queryFn: async () => {
      const response = await api.getCashgame(id);
      return new DetailedCashgame(response.data);
    },
    staleTime: thirtySecondsStaleTime,
    refetchInterval: fifteenSecondsRefreshInterval,
  });
};
