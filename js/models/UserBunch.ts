import { Role } from './Role';

// todo: Make the properties lowercase

export interface UserBunch {
  id: string;
  slug: string;
  name: string;
  playerId: string;
  playerName: string;
  role: Role;
}
