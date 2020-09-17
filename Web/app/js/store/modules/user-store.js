import api from '@/api';
import roles from '@/roles';
import auth from '@/auth';
import { UserStoreGetters, UserStoreActions, UserStoreMutations } from '@/store/helpers/UserStoreHelpers';

export default {
    namespaced: false,
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
        [UserStoreGetters.IsSignedIn]: state => state._isSignedIn,
        [UserStoreGetters.IsAdmin]: state => state._role === roles.admin,
        [UserStoreGetters.UserName]: state => state._userName,
        [UserStoreGetters.DisplayName]: state => state._displayName,
        [UserStoreGetters.UserReady]: state => state._userReady,
        [UserStoreGetters.Users]: state => state._users,
        [UserStoreGetters.UsersReady]: state => state._usersReady
    },
    actions: {
        [UserStoreActions.LoadUser]: function(context) {
            if (!context.state._userInitialized) {
                const isLoggedIn = auth.isLoggedIn();
                context.commit(UserStoreMutations.SetUserInitialized);
                context.commit(UserStoreMutations.SetIsSignedIn, isLoggedIn);
                if (isLoggedIn) {
                    api.getUser()
                        .then(function(response) {
                            context.commit(UserStoreMutations.SetUser, response.data);
                        })
                        .catch(function() {
                            context.commit(UserStoreMutations.SetUserError);
                        });
                } else {
                    context.commit(UserStoreMutations.SetUserError);
                }
            }
        },
        [UserStoreActions.LoadUsers]: function(context) {
            context.commit(UserStoreMutations.SetUserInitialized);
            api.getUsers()
                .then(function (response) {
                    context.commit(UserStoreMutations.SetUsers, response.data);
                })
                .catch(function (error) {
                    context.commit(UserStoreMutations.SetUsersError);
                });
        }
    },
    mutations: {
        [UserStoreMutations.SetIsSignedIn]: function(state, isSignedIn) {
            state._isSignedIn = isSignedIn;
        },
        [UserStoreMutations.SetUser]: function(state, user) {
            state._userName = user.userName;
            state._displayName = user.displayName;
            state._role = user.role;
            state._userReady = true;
        },
        [UserStoreMutations.SetUserError]: function(state) {
            state._userName = '';
            state._displayName = '';
            state._role = roles.none;
            state._userReady = true;
        },
        [UserStoreMutations.SetUserInitialized]: function(state) {
            state._userInitialized = true;
        },
        [UserStoreMutations.SetUsers]: function(state, users) {
            state._users = users;
            state._usersReady = true;
        },
        [UserStoreMutations.SetUsersError]: function(state) {
            state._usersReady = true;
        },
        [UserStoreMutations.SetUsersInitialized]: function(state) {
            state._usersInitialized = true;
        }
    }
};
