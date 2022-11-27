import { LocationResponse } from './LocationResponse';

export interface EventResponse {
  id: string;
  bunchId: string;
  name: string;
  startDate: string;
  location: LocationResponse;
}
