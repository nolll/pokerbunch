import { LocationResponse } from '@/response/LocationResponse';

export enum LocationStoreGetters {
    Slug = 'location_slug',
    Locations = 'location_locations',
    LocationsReady = 'location_locationsReady'
}

export enum LocationStoreActions {
    LoadLocations = 'location_loadLocations',
    AddLocation = 'location_addLocation'
}

export enum LocationStoreMutations {
    SetLocationsData = 'location_setLocationsData',
    SetInitialized = 'location_setInitialized',
    AddLocation = 'location_addLocation'
}

export interface LocationStoreState {
    _slug: string;
    _locations: LocationResponse[];
    _locationsReady: boolean;
    _initialized: boolean;
}

export interface AddLocationParams{
    bunchId: string;
    name: string;
}
