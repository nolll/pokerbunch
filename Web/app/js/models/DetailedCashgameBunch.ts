import { Role } from '@/models/Role';

export interface DetailedCashgameBunch {
    id: string;
    timezone: string;
    currencyFormat: string;
    currencySymbol: string;
    currencyLayout: string;
    thousandSeparator: string;
    culture: string;
    role: Role;
}
