import { Role } from './models/Role';
import roles from './roles';

const isManager = (role: Role | null | undefined) => role === roles.manager;
const isAdmin = (role: Role | null | undefined) => role === roles.admin;
const isAdminOrManager = (role: Role | null | undefined) => isAdmin(role) || isManager(role);

export default {
  canEditBunch: (role: Role) => isAdminOrManager(role),
  canListBunches: (role: Role | null | undefined) => isAdmin(role),
  canSeeAdminMenu: (role: Role | null | undefined) => isAdmin(role),
  canSelectPlayer: (role: Role) => isAdminOrManager(role),
  canEditCashgame: (role: Role) => isAdminOrManager(role),
  canClearCache: (role: Role | null | undefined) => isAdmin(role),
  canSendTestEmail: (role: Role | null | undefined) => isAdmin(role),
};
