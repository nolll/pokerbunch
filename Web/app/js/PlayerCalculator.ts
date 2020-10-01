import dayjs from 'dayjs';
import relativeTime from 'dayjs/plugin/relativeTime';
import { DetailedCashgameResponsePlayer } from './response/DetailedCashgameResponsePlayer';
import { DetailedCashgameResponseActionType } from './response/DetailedCashgameResponseActionType';

dayjs.extend(relativeTime);

export default {
    getLastReportTime(player: DetailedCashgameResponsePlayer) {
        if (player.actions.length === 0)
            return dayjs().fromNow();
        return dayjs(player.actions[player.actions.length - 1].time).fromNow();
    },
    getBuyin(player: DetailedCashgameResponsePlayer) {
        if (player.actions.length === 0)
            return 0;
        let sum = 0;
        for (let i = 0; i < player.actions.length; i++) {
            const added = player.actions[i].added || 0;
            sum += added;
        }
        return sum;
    },
    getStack(player: DetailedCashgameResponsePlayer) {
        const c = player.actions;
        if (c.length === 0)
            return 0;
        return c[c.length - 1].stack;
    },
    getWinnings(player: DetailedCashgameResponsePlayer) {
        return this.getStack(player) - this.getBuyin(player);
    },
    hasCashedOut(player: DetailedCashgameResponsePlayer) {
        return !!player.actions.find(a => a.type === DetailedCashgameResponseActionType.Cashout);
    }
};
