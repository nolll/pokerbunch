import { ArchiveCashgameLocationResponse } from '@/response/ArchiveCashgameLocationResponse';

export class ArchiveCashgameLocation {
  id: string;
  name: string;

  constructor(id: string, name: string) {
    this.id = id;
    this.name = name;
  }

  public static fromResponse(response: ArchiveCashgameLocationResponse) {
    return new ArchiveCashgameLocation(response.id, response.name);
  }
}
