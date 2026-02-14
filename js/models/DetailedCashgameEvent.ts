import { DetailedCashgameResponseEvent } from '@/response/DetailedCashgameResponseEvent';

export class DetailedCashgameEvent {
  id: string;
  name: string;

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }

  public static fromResponse(response: DetailedCashgameResponseEvent) {
    return new DetailedCashgameEvent(response.id, response.name);
  }
}
