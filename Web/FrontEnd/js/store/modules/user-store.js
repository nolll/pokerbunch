import api from '@/api';

export default {
    namespaced: true,
    state: {
        _isSignedIn: false,
        _userName: '',
        _displayName: '',
        _userReady: false,
        _initialized: false,
        _users: [],
        _usersReady: false
    },
    getters: {
        isSignedIn: state => state._isSignedIn,
        userName: state => state._userName,
        displayName: state => state._displayName,
        userReady: state => state._userReady,
        users: state => state._users,
        usersReady: state => state._usersReady
    },
    actions: {
        loadUser(context) {
            if (!context.state._initialized) {
                context.commit('setInitialized');
                api.getUser()
                    .then(function (response) {
                        context.commit('setUser', response.data);
                    })
                    .catch(function (error) {
                        context.commit('setUserError');
                    });
            }
        },
        loadUsers(context) {
            api.getUsers()
                .then(function (response) {
                    context.commit('setUsers', response.data);
                })
                .catch(function (error) {
                    context.commit('setUsersError');
                });
        }
    },
    mutations: {
        setUser(state, user) {
            state._isSignedIn = true;
            state._userName = user.userName;
            state._displayName = user.displayName;
            state._userReady = true;
        },
        setUserError(state) {
            state._isSignedIn = false;
            state._userName = '';
            state._displayName = '';
            state._userReady = true;
        },
        setInitialized(state) {
            state._initialized = true;
        },
        setUsers(state, users) {
            state._users = users;
            state._usersReady = true;
        },
        setUsersError(state) {
            state._usersReady = true;
        }
    }
};
