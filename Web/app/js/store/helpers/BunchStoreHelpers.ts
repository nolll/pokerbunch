import { Role } from '@/models/Role';
import { BunchResponse } from '@/response/BunchResponse';

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

export interface BunchStoreState{
    _slug: string;
    _name: string;
    _currencyFormat: string;
    _thousandSeparator: string;
    _description: string;
    _houseRules: string;
    _defaultBuyin: number;
    _role: Role;
    _playerId: string | null;
    _bunchReady: boolean;
    _bunchInitialized: boolean;
    _userBunches: BunchResponse[];
    _userBunchesReady: boolean;
    _userBunchesInitialized: boolean;
}

export interface LoadBunchParams{
    slug: string;
}