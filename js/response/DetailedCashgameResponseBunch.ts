import { Role } from '@/models/Role';

export interface DetailedCashgameResponseBunch {
  id: string;
  timezone: string;
  currencyFormat: string;
  currencySymbol: string;
  currencyLayout: string;
  thousandSeparator: string;
  role: Role;
}
