export enum BunchStoreGetters {
    Slug = 'bunch_slug',
    Name = 'bunch_name',
    CurrencyFormat = 'bunch_currencyFormat',
    ThousandSeparator = 'bunch_thousandSeparator',
    Description = 'bunch_description',
    HouseRules = 'bunch_houseRules',
    DefaultBuyin = 'bunch_defaultBuyin',
    PlayerId = 'bunch_playerId',
    IsManager = 'bunch_isManager',
    BunchReady = 'bunch_bunchReady',
    UserBunches = 'bunch_userBunches',
    UserBunchesReady = 'bunch_userBunchesReady'
}

export enum BunchStoreActions {
    LoadBunch = 'bunch_loadBunch',
    LoadUserBunches = 'bunch_loadUserBunches'
}

export enum BunchStoreMutations {
    SetBunchData = 'bunch_setBunchData',
    SetBunchInitialized = 'bunch_setBunchInitialized',
    SetUserBunchesData = 'bunch_setUserBunchesData',
    SetUserBunchesError = 'bunch_setUserBunchesError',
    SetUserBunchesInitialized = 'bunch_setUserBunchesInitialized'
}
