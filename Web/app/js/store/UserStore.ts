import { StoreOptions } from 'vuex';
import api from '@/api';
import roles from '@/roles';
import auth from '@/auth';
import { UserStoreGetters, UserStoreActions, UserStoreMutations, UserStoreState } from '@/store/helpers/UserStoreHelpers';
import { User } from '@/models/User';
import { Role } from '@/models/Role';

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
        async [UserStoreActions.LoadCurrentUser](context) {
            if (!context.state._userInitialized) {
                const isLoggedIn = auth.isLoggedIn();
                context.commit(UserStoreMutations.SetUserInitialized);
                context.commit(UserStoreMutations.SetIsSignedIn, isLoggedIn);
                if (isLoggedIn) {
                    try{
                        const response = await api.getCurrentUser();
                        context.commit(UserStoreMutations.SetUser, response.data);
                    } catch {
                        context.commit(UserStoreMutations.SetUserError);
                    }
                } else {
                    context.commit(UserStoreMutations.SetUserError);
                }
            }
        },
        async [UserStoreActions.LoadUsers](context) {
            context.commit(UserStoreMutations.SetUserInitialized);
            try{
                const response = await api.getUsers();
                context.commit(UserStoreMutations.SetUsers, response.data);
            } catch(error){
                context.commit(UserStoreMutations.SetUsersError);
            }
        }
    },
    mutations: {
        [UserStoreMutations.SetIsSignedIn](state, isSignedIn) {
            state._isSignedIn = isSignedIn;
        },
        [UserStoreMutations.SetUser](state, user: User) {
            state._userName = user.userName;
            state._displayName = user.displayName;
            state._role = user.role;
            state._userReady = true;
        },
        [UserStoreMutations.SetUserError](state) {
            state._userName = '';
            state._displayName = '';
            state._role = Role.None;
            state._userReady = true;
        },
        [UserStoreMutations.SetUserInitialized](state) {
            state._userInitialized = true;
        },
        [UserStoreMutations.SetUsers](state, users: User[]) {
            state._users = users;
            state._usersReady = true;
        },
        [UserStoreMutations.SetUsersError](state) {
            state._usersReady = true;
        },
        [UserStoreMutations.SetUsersInitialized](state) {
            state._usersInitialized = true;
        }
    }
} as StoreOptions<UserStoreState>;
