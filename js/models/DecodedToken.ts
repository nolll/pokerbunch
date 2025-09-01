import { UserBunch } from './UserBunch';

export interface DecodedToken {
  version: string;
  unique_name: string;
  userdisplayname: string;
  isadmin: string;
  bunches: UserBunch[];
}
