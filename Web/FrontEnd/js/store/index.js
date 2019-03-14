import vue from 'vue';
import vuex from 'vuex';
import { UserStore, BunchStore, CashgameStore, CurrentGameStore, GameArchiveStore, PlayerStore } from './modules';

vue.use(vuex);

export default new vuex.Store({
    strict: true,
    state: {
        
    },
    getters: {
        
    },
    actions: {
        
    },
    mutations: {
        
    },
    modules: {
        user: UserStore,
        bunch: BunchStore,
        cashgame: CashgameStore,
        currentGame: CurrentGameStore,
        gameArchive: GameArchiveStore,
        player: PlayerStore
    }
});
