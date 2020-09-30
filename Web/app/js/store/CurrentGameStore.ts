import { StoreOptions } from 'vuex';
import api from '@/api';
import { CurrentGameStoreGetters, CurrentGameStoreActions, CurrentGameStoreMutations, CurrentGameStoreState } from '@/store/helpers/CurrentGameStoreHelpers';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';

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
        async [CurrentGameStoreActions.LoadCurrentGames](context, { slug }) {
            try{
                const response = await api.getCurrentGames(slug)
                if (response.status === 200) {
                    context.commit(CurrentGameStoreMutations.DataLoaded, response.data);
                }
                context.commit(CurrentGameStoreMutations.LoadingComplete);
            } catch {
                context.commit(CurrentGameStoreMutations.LoadingComplete);
            }
        }
    },
    mutations: {
        [CurrentGameStoreMutations.LoadingComplete](state) {
            state._currentGamesReady = true;
        },
        [CurrentGameStoreMutations.DataLoaded](state, data: CurrentGameResponse[]) {
            state._currentGames = data;
        }
    }
} as StoreOptions<CurrentGameStoreState>;
