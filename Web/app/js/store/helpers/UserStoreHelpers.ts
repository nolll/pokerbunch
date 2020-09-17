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
    LoadUser = 'user_loadUser',
    LoadUsers = 'user_loadUsers'
}

export enum UserStoreMutations {
    SetIsSignedIn = 'user_setIsSignedIn',
    SetUser = 'user_setUser',
    SetUserError = 'user_setUserError',
    SetUserInitialized = 'user_setUserInitialized',
    SetUsers = 'user_setUsers',
    SetUsersError = 'user_setUsersError',
    SetUsersInitialized = 'user_setUsersInitialized'
}
