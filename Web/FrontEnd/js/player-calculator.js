import moment from 'moment';
import actionTypes from '@/action-types';

export default {
    getLastReportTime(player) {
        if (player.checkpoints.length === 0)
            return moment().fromNow();
        return moment(player.checkpoints[player.checkpoints.length - 1].time).fromNow();
    },
    getBuyin(player) {
        if (player.checkpoints.length === 0)
            return 0;
        let sum = 0;
        for (let i = 0; i < player.checkpoints.length; i++) {
            sum += player.checkpoints[i].addedMoney;
        }
        return sum;
    },
    getStack(player) {
        const c = player.checkpoints;
        if (c.length === 0)
            return 0;
        return c[c.length - 1].stack;
    },
    getWinnings(player) {
        return this.getStack(player) - this.getBuyin(player);
    },
    hasCashedOut(player) {
        return !!player.checkpoints.find(a => a.type === actionTypes.cashout);
    }
};
