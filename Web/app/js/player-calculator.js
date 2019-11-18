import moment from 'moment';
import actionTypes from '@/action-types';

export default {
    getLastReportTime(player) {
        if (player.actions.length === 0)
            return moment().fromNow();
        return moment(player.actions[player.actions.length - 1].time).fromNow();
    },
    getBuyin(player) {
        if (player.actions.length === 0)
            return 0;
        let sum = 0;
        for (let i = 0; i < player.actions.length; i++) {
            const added = player.actions[i].added || 0;
            sum += added;
        }
        return sum;
    },
    getStack(player) {
        const c = player.actions;
        if (c.length === 0)
            return 0;
        return c[c.length - 1].stack;
    },
    getWinnings(player) {
        return this.getStack(player) - this.getBuyin(player);
    },
    hasCashedOut(player) {
        return !!player.actions.find(a => a.type === actionTypes.cashout);
    }
};
