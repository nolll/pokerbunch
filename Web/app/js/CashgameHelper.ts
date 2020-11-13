import { DetailedCashgameResponsePlayer } from './response/DetailedCashgameResponsePlayer';

export default {
    getTotalBuyin(players: DetailedCashgameResponsePlayer[]){
        let sum = 0;
        for (const player of players) {
            let buyin = 0;
            if (player.actions.length === 0)
                continue;

            for (const action of player.actions) {
                buyin += action.added || 0;
            }
            sum += buyin;
        }
        return sum;
    },
    getTotalStacks(players: DetailedCashgameResponsePlayer[]){
        let sum = 0;
        for (const player of players) {
            const c = player.actions;
            sum += c.length > 0 ? c[c.length - 1].stack : 0;
        }
        return sum;
    }
};
