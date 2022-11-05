import { Role } from '@/models/Role';
import { BunchResponse } from '@/response/BunchResponse';

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

export interface BunchStoreState {
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
