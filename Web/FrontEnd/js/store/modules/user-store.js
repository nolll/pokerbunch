import api from '@/api';

export default {
    namespaced: true,
    state: {
        _isSignedIn: false,
        _userName: '',
        _displayName: '',
        _ready: false,
        _initialized: false
    },
    getters: {
        isSignedIn(state) {
            return state._isSignedIn;
        },
        userName(state) {
            return state._userName;
        },
        displayName(state) {
            return state._displayName;
        },
        userReady(state) {
            return state._ready;
        }
    },
    actions: {
        loadUser(context) {
            if (!context.state._initialized) {
                context.commit('setUserInitialized');
                api.getUser()
                    .then(function (response) {
                        context.commit('setData', response.data);
                    })
                    .catch(function(error) {
                        context.commit('setErrorData');
                    });
            }
        }
    },
    mutations: {
        setData(state, user) {
            state._isSignedIn = true;
            state._userName = user.userName;
            state._displayName = user.displayName;
            state._ready = true;
        },
        setErrorData(state) {
            state._isSignedIn = false;
            state._userName = '';
            state._displayName = '';
            state._ready = true;
        },
        setUserInitialized(state) {
            state._initialized = true;
        }
    }
};
