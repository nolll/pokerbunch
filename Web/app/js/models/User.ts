import { Role } from './Role';

export interface User {
  userName: string;
  displayName: string;
  realName: string | null;
  role: Role | null;
  email: string | null;
  avatar: string | undefined;
}
