import { Role } from '@/models/Role';
import { BunchResponse } from '@/response/BunchResponse';

export enum BunchStoreGetters {
    Slug = 'bunch_slug',
    Bunch = 'bunch_bunch',
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
    UserBunchesReady = 'bunch_userBunchesReady',
    Bunches = 'bunch_bunches',
    BunchesReady = 'bunch_bunchesReady'
}

export enum BunchStoreActions {
    LoadBunch = 'bunch_loadBunch',
    LoadUserBunches = 'bunch_loadUserBunches',
    LoadBunches = 'bunch_loadBunches'
}

export enum BunchStoreMutations {
    SetBunchData = 'bunch_setBunchData',
    SetBunchReady = 'bunch_setBunchReady',
    SetUserBunchesData = 'bunch_setUserBunchesData',
    SetUserBunchesError = 'bunch_setUserBunchesError',
    SetUserBunchesReady = 'bunch_setUserBunchesReady',
    SetBunchesData = 'bunch_setBunchesData',
    SetBunchesError = 'bunch_setBunchesError',
    SetBunchesReady = 'bunch_setBunchesReady',
}

export interface BunchStoreState{
    _bunch: BunchResponse | null;
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
    _userBunches: BunchResponse[];
    _userBunchesReady: boolean;
    _bunches: BunchResponse[];
    _bunchesReady: boolean;
}

export interface LoadBunchParams{
    slug: string;
    forceLoad: boolean | undefined;
}