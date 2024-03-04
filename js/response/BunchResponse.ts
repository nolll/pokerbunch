import { Role } from '@/models/Role';
import { BunchResponsePlayer } from './BunchResponsePlayer';

export interface BunchResponse {
  id: string;
  name: string;
  description: string;
  houseRules: string;
  timezone: string;
  currencySymbol: string;
  currencyLayout: string;
  currencyFormat: string;
  thousandSeparator: string;
  defaultBuyin: number;
  player: BunchResponsePlayer;
  role: Role;
}
