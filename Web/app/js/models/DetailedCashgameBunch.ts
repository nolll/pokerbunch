import { Role } from '@/models/Role';
import { DetailedCashgameResponseBunch } from '@/response/DetailedCashgameResponseBunch';

export class DetailedCashgameBunch {
    id: string;
    timezone: string;
    currencyFormat: string;
    currencySymbol: string;
    currencyLayout: string;
    thousandSeparator: string;
    culture: string;
    role: Role;

    constructor(response: DetailedCashgameResponseBunch) {
        this.id = response.id;
        this.timezone = response.timezone;
        this.currencyFormat = response.currencyFormat;
        this.currencySymbol = response.currencySymbol;
        this.currencyLayout = response.currencyLayout;
        this.thousandSeparator = response.thousandSeparator;
        this.culture = response.culture;
        this.role = response.role;
    }
}
