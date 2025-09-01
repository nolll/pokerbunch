import { UserBunch } from './UserBunch';

export interface CurrentUser {
  tokenVersion: string;
  isSignedIn: boolean;
  userName: string;
  userDisplayName: string;
  isAdmin: boolean;
  bunches: UserBunch[];
}
