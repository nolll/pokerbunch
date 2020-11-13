import { DetailedCashgameResponseLocation } from '@/response/DetailedCashgameResponseLocation';

export class DetailedCashgameLocation {
    id: string;
    name: string;

    constructor(response: DetailedCashgameResponseLocation) {
        this.id = response.id;
        this.name = response.name;
    }
}
