import { StoreOptions } from 'vuex';
import roles from '@/roles';
import { UserStoreMutations, UserStoreState } from '@/store/helpers/UserStoreHelpers';
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
    },
    [UserStoreMutations.SetUsersReady](state, isReady: boolean) {
      state._usersReady = isReady;
    },
  },
} as StoreOptions<UserStoreState>;
