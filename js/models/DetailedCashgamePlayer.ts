import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';
import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import relativeTime from 'dayjs/plugin/relativeTime';
import { DetailedCashgameAction } from './DetailedCashgameAction';

dayjs.extend(utc);
dayjs.extend(relativeTime);

export class DetailedCashgamePlayer {
  id: string;
  name: string;
  color: string;
  actions: DetailedCashgameAction[];

  constructor(id: string, name: string, color: string, actions: DetailedCashgameAction[]) {
    this.id = id;
    this.name = name;
    this.color = color;
    this.actions = actions.map((o) => new DetailedCashgameAction(o));
  }

  public static new(playerId: string, playerName: string, playerColor: string): DetailedCashgamePlayer {
    return new DetailedCashgamePlayer(playerId, playerName, playerColor, []);
  }

  public static fromResponse(response: DetailedCashgameResponsePlayer) {
    return new DetailedCashgamePlayer(
      response.id,
      response.name,
      response.color,
      response.actions.map((o) => new DetailedCashgameAction(o))
    );
  }

  public getLastReportTime() {
    if (this.actions.length === 0) return dayjs().fromNow();
    return dayjs(this.actions[this.actions.length - 1].time).fromNow();
  }

  public getBuyin() {
    if (this.actions.length === 0) return 0;
    let sum = 0;
    for (const action of this.actions) {
      const added = action.added || 0;
      sum += added;
    }
    return sum;
  }

  public getStack() {
    const c = this.actions;
    if (c.length === 0) return 0;
    return c[c.length - 1].stack;
  }

  public getWinnings() {
    return this.getStack() - this.getBuyin();
  }

  public hasCashedOut() {
    return this.cashouts().length > 0;
  }

  public buyins() {
    return this.actions.filter((o) => o.type === DetailedCashgameResponseActionType.Buyin);
  }

  public reports() {
    return this.actions.filter((o) => o.type === DetailedCashgameResponseActionType.Report);
  }

  public cashouts() {
    return this.actions.filter((o) => o.type === DetailedCashgameResponseActionType.Cashout);
  }
}
