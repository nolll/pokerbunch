import { Role } from '@/models/Role';
import { User } from '@/models/User';

export enum UserStoreGetters {
    IsSignedIn = 'user_isSignedIn',
    IsAdmin = 'user_isAdmin',
    UserName = 'user_userName',
    DisplayName = 'user_displayName',
    UserReady = 'user_userReady',
    Users = 'user_users',
    UsersReady = 'user_usersReady'
}

export enum UserStoreActions {
    LoadCurrentUser = 'user_loadCurrentUser',
    LoadUsers = 'user_loadUsers'
}

export enum UserStoreMutations {
    SetIsSignedIn = 'user_setIsSignedIn',
    SetUser = 'user_setUser',
    SetUserError = 'user_setUserError',
    SetUserInitialized = 'user_setUserInitialized',
    SetUsers = 'user_setUsers',
    SetUsersError = 'user_setUsersError',
    SetUsersReady = 'user_setUsersReady'
}

export interface UserStoreState{
    _isSignedIn: boolean;
    _userName: string;
    _displayName: string;
    _role: Role | null;
    _userReady: boolean;
    _userInitialized: boolean;
    _users: User[];
    _usersReady: boolean;
}