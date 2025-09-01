import { UserBunch } from './UserBunch';

export interface DecodedToken {
  unique_name: string;
  userdisplayname: string;
  isadmin: string;
  bunches: UserBunch[];
}
