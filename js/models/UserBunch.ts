import { Role } from './Role';

export interface UserBunch {
  id: string;
  slug: string;
  name: string;
  playerId: string;
  playerName: string;
  role: Role;
}
