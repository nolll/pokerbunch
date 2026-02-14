import { DetailedCashgameResponseLocation } from '@/response/DetailedCashgameResponseLocation';

export class DetailedCashgameLocation {
  id: string;
  name: string;

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }

  public static fromResponse(response: DetailedCashgameResponseLocation) {
    return new DetailedCashgameLocation(response.id, response.name);
  }
}
