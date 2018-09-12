'use strict';

import Vue from 'vue';
import Vuex from 'vuex';
import { CurrentGame, GameList, TopList } from './modules';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        
    },
    getters: {
        
    },
    actions: {
        
    },
    mutations: {
        
    },
    modules: {
        currentGame: CurrentGame,
        gameList: GameList,
        topList: TopList
    }
});
