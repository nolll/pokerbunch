import api from '@/api';
import roles from '@/roles';

export default {
    namespaced: true,
    state: {
        _isSignedIn: false,
        _userName: '',
        _displayName: '',
        _role: roles.none,
        _userReady: false,
        _userInitialized: false,
        _users: [],
        _usersReady: false,
        _usersInitialized: false
    },
    getters: {
        isSignedIn: state => state._isSignedIn,
        isAdmin: state => state._role === roles.admin,
        userName: state => state._userName,
        displayName: state => state._displayName,
        userReady: state => state._userReady,
        users: state => state._users,
        usersReady: state => state._usersReady
    },
    actions: {
        loadUser(context) {
            if (!context.state._userInitialized) {
                context.commit('setUserInitialized');
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
            context.commit('setUserInitialized');
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
            state._role = user.role;
            state._userReady = true;
        },
        setUserError(state) {
            state._isSignedIn = false;
            state._userName = '';
            state._displayName = '';
            state._role = roles.none;
            state._userReady = true;
        },
        setUserInitialized(state) {
            state._userInitialized = true;
        },
        setUsers(state, users) {
            state._users = users;
            state._usersReady = true;
        },
        setUsersError(state) {
            state._usersReady = true;
        },
        setUsersInitialized(state) {
            state._usersInitialized = true;
        }
    }
};
