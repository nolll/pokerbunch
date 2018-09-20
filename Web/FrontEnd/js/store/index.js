'use strict';

import Vue from 'vue';
import Vuex from 'vuex';
import { CurrentGameStore, GameListStore, TopListStore } from './modules';

Vue.use(Vuex);

export default new Vuex.Store({
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
        currentGame: CurrentGameStore,
        gameList: GameListStore,
        topList: TopListStore
    }
});
