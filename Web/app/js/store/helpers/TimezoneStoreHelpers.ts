import { Timezone } from '@/models/Timezone';

export enum TimezoneStoreGetters {
    Timezones = 'timezone_timezones',
    TimezonesReady = 'timezone_timezonesReady'
}

export enum TimezoneStoreActions {
    LoadTimezones = 'timezone_loadTimezones'
}

export enum TimezoneStoreMutations {
    SetTimezonesData = 'timezone_setTimezonesData',
    SetInitialized = 'timezone_setInitialized'
}

export interface TimezoneStoreState {
    _timezones: Timezone[];
    _timezonesReady: boolean;
    _initialized: boolean;
}
