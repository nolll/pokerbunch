import vue from 'vue';
import vuex from 'vuex';
import { UserStore, BunchStore, CurrentGameStore, GameArchiveStore } from './modules';

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
        currentGame: CurrentGameStore,
        gameArchive: GameArchiveStore
    }
});
