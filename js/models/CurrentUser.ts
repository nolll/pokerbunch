import { UserBunch } from './UserBunch';

export interface CurrentUser {
  isSignedIn: boolean;
  userName: string;
  userDisplayName: string;
  isAdmin: boolean;
  bunches: UserBunch[];
}
