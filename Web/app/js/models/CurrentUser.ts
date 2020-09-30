import { Role } from './Role';

export interface CurrentUser {
    userName: string;
    displayName: string;
    url: string;
    role: Role;
    email: string;
}
