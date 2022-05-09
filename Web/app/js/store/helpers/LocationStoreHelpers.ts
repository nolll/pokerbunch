import { LocationResponse } from '@/response/LocationResponse';

export enum LocationStoreMutations {
  SetLocationsData = 'location_setLocationsData',
  SetSlug = 'location_setSlug',
  AddLocation = 'location_addLocation',
}

export interface LocationStoreState {
  _slug: string;
  _locations: LocationResponse[];
  _locationsReady: boolean;
}
