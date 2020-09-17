import api from '@/api';
import { CurrentGameStoreGetters, CurrentGameStoreActions, CurrentGameStoreMutations } from '@/store/helpers/CurrentGameStoreHelpers';

export default {
    namespaced: false,
    state: {
        _currentGames: [],
        _currentGamesReady: false
    },
    getters: {
        [CurrentGameStoreGetters.CurrentGamesReady](state) {
            return state._currentGamesReady;
        },
        [CurrentGameStoreGetters.CurrentGames](state) {
            return state._currentGames;
        }
    },
    actions: {
        [CurrentGameStoreActions.LoadCurrentGames](context, { slug }) {
            api.getCurrentGames(slug)
                .then(function (response) {
                    if (response.status === 200) {
                        context.commit(CurrentGameStoreMutations.DataLoaded, response.data);
                    }
                    context.commit(CurrentGameStoreMutations.LoadingComplete);
                })
                .catch(function () {
                    context.commit(CurrentGameStoreMutations.LoadingComplete);
                });
        }
    },
    mutations: {
        [CurrentGameStoreMutations.LoadingComplete](state) {
            state._currentGamesReady = true;
        },
        [CurrentGameStoreMutations.DataLoaded](state, data) {
            state._currentGames = data;
        }
    }
};
