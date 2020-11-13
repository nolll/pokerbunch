import { DetailedCashgameResponseEvent } from '@/response/DetailedCashgameResponseEvent';

export class DetailedCashgameEvent {
    id: string;
    name: string;

    constructor(response: DetailedCashgameResponseEvent) {
        this.id = response.id;
        this.name = response.name;
    }
}
