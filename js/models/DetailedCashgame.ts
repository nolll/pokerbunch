import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
import dayjs from 'dayjs';
import { DetailedCashgameBunch } from './DetailedCashgameBunch';
import { DetailedCashgameEvent } from './DetailedCashgameEvent';
import { DetailedCashgameLocation } from './DetailedCashgameLocation';
import { DetailedCashgamePlayer } from './DetailedCashgamePlayer';

export class DetailedCashgame {
  id: string;
  bunch: DetailedCashgameBunch;
  location: DetailedCashgameLocation;
  startTime: Date;
  updatedTime: Date;
  players: DetailedCashgamePlayer[];
  event: DetailedCashgameEvent | null;

  constructor(response: DetailedCashgameResponse) {
    this.id = response.id;
    this.bunch = new DetailedCashgameBunch(response.bunch);
    this.location = DetailedCashgameLocation.fromResponse(response.location);
    this.startTime = dayjs(response.startTime).toDate();
    this.updatedTime = dayjs(response.updatedTime).toDate();
    this.players = response.players.map((o) => DetailedCashgamePlayer.fromResponse(o));
    this.event = !!response.event ? DetailedCashgameEvent.fromResponse(response.event) : null;
  }

  public get isRunning() {
    if (this.players.length === 0) return true;

    for (const player of this.players) {
      if (!player.hasCashedOut()) return true;
    }
    return false;
  }
}
