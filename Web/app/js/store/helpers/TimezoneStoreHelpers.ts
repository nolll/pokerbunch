import { Timezone } from '@/models/Timezone';

export enum TimezoneStoreMutations {
  SetTimezonesData = 'timezone_setTimezonesData',
}

export interface TimezoneStoreState {
  _timezones: Timezone[];
  _timezonesReady: boolean;
}
