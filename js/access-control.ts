import { Role } from './models/Role';
import roles from './roles';

export default {
  canEditBunch: (role: Role) => {
    return role === roles.manager || role === roles.admin;
  },
};
