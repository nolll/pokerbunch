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
        isSignedIn: state => state._isSignedIn,
        userName: state => state._userName,
        displayName: state => state._displayName,
        userReady: state => state._ready
    },
    actions: {
        loadUser(context) {
            if (!context.state._initialized) {
                context.commit('setInitialized');
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
        setInitialized(state) {
            state._initialized = true;
        }
    }
};
