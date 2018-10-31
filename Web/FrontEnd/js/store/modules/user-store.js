'use strict';

import api from '../../api';

export default {
    namespaced: true,
    state: {
        isSignedIn: false,
        userName: '',
        displayName: '',
        userReady: false,
        userInitialized: false
    },
    getters: {
        
    },
    actions: {
        loadUser(context) {
            if (!context.state.userInitialized) {
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
            state.isSignedIn = true;
            state.userName = user.userName;
            state.displayName = user.displayName;
            state.userReady = true;
        },
        setErrorData(state) {
            state.isSignedIn = false;
            state.userName = '';
            state.displayName = '';
            state.userReady = true;
        },
        setUserInitialized(state) {
            state.userInitialized = true;
        }
    }
};
