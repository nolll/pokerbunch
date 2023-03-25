import { Role } from './models/Role';
import roles from './roles';

export default {
  canEditBunch: (role: Role) => {
    return role === roles.manager || role === roles.admin;
  },
  canListBunches: (role: Role | null | undefined) => {
    return role === roles.admin;
  },
  canSeeAdminMenu: (role: Role | null | undefined) => {
    return role === roles.admin;
  },
  canSelectPlayer: (role: Role) => {
    return role === roles.manager || role === roles.admin;
  },
  canEditCashgame: (role: Role) => {
    return role === roles.manager || role === roles.admin;
  },
};
