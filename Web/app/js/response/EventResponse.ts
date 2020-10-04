import { LocationResponse } from './LocationResponse';

export interface EventResponse{
    id: number;
    bunchId: string;
    name: string;
    startDate: string;
    location: LocationResponse;
}
