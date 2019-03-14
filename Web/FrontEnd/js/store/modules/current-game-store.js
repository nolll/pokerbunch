import api from '@/api';

export default {
    namespaced: true,
    state: {
        _currentGames: [],
        _currentGamesReady: false
    },
    getters: {
        currentGamesReady(state) {
            return state._currentGamesReady;
        },
        currentGames(state) {
            return state._currentGames;
        }
    },
    actions: {
        loadCurrentGames(context, { slug }) {
            api.getCurrentGames(slug)
                .then(function (response) {
                    if (response.status === 200) {
                        context.commit('dataLoaded', response.data);
                    }
                    context.commit('loadingComplete');
                })
                .catch(function () {
                    context.commit('loadingComplete');
                });
        }
    },
    mutations: {
        loadingComplete(state) {
            state._currentGamesReady = true;
        },
        dataLoaded(state, data) {
            state._currentGames = data;
        }
    }
};
