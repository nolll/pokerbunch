import { Component, Vue } from 'vue-property-decorator';
import { CashgameStoreGetters, CashgameStoreActions } from '@/store/helpers/CashgameStoreHelpers';
import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';

@Component
export class CashgameMixin extends Vue {
    protected get $_isInGame(): boolean {
        return this.$store.getters[CashgameStoreGetters.IsInGame];
    }

    protected get $_playerId(): string {
        return this.$store.getters[CashgameStoreGetters.PlayerId];
    }

    protected get $_cashgameId(): string {
        return this.$store.getters[CashgameStoreGetters.CashgameId];
    }

    protected get $_totalBuyin(): number {
        return this.$store.getters[CashgameStoreGetters.TotalBuyin];
    }

    protected get $_totalStacks(): number {
        return this.$store.getters[CashgameStoreGetters.TotalStacks];
    }

    protected get $_isRunning(): boolean {
        return this.$store.getters[CashgameStoreGetters.IsRunning];
    }

    protected get $_locationId(): string {
        return this.$store.getters[CashgameStoreGetters.LocationId];
    }

    protected get $_locationName(): string {
        return this.$store.getters[CashgameStoreGetters.LocationName];
    }

    protected get $_reportFormVisible(): boolean {
        return this.$store.getters[CashgameStoreGetters.ReportFormVisible];
    }

    protected get $_buyinFormVisible(): boolean {
        return this.$store.getters[CashgameStoreGetters.BuyinFormVisible];
    }

    protected get $_cashoutFormVisible(): boolean {
        return this.$store.getters[CashgameStoreGetters.CashoutFormVisible];
    }

    protected get $_cashgamePlayers(): DetailedCashgameResponsePlayer[] {
        return this.$store.getters[CashgameStoreGetters.Players];
    }

    protected get $_hasPlayers(): boolean {
        return this.$store.getters[CashgameStoreGetters.HasPlayers];
    }

    protected get $_startTime(): Date {
        return this.$store.getters[CashgameStoreGetters.StartTime];
    }

    protected get $_updatedTime(): Date {
        return this.$store.getters[CashgameStoreGetters.UpdatedTime];
    }

    protected get $_sortedPlayers(): DetailedCashgameResponsePlayer[] {
        return this.$store.getters[CashgameStoreGetters.SortedPlayers];
    }

    protected get $_canCashout(): boolean {
        return this.$store.getters[CashgameStoreGetters.CanCashout];
    }

    protected get $_canReport(): boolean {
        return this.$store.getters[CashgameStoreGetters.CanReport];
    }

    protected get $_canBuyin(): boolean {
        return this.$store.getters[CashgameStoreGetters.CanBuyin];
    }

    protected get $_cashgameReady(): boolean {
        return this.$store.getters[CashgameStoreGetters.CashgameReady];
    }

    protected $_loadCashgame() {
        this.$store.dispatch(CashgameStoreActions.LoadCashgame, { id: this.$route.params.id });
    }

    protected $_showReportForm() {
        this.$store.dispatch(CashgameStoreActions.ShowReportForm);
    }

    protected $_showBuyinForm() {
        this.$store.dispatch(CashgameStoreActions.ShowBuyinForm);
    }

    protected $_showCashoutForm() {
        this.$store.dispatch(CashgameStoreActions.ShowCashoutForm);
    }

    protected $_buyin(amount: number, stack: number) {
        this.$store.dispatch(CashgameStoreActions.Buyin, { amount: amount, stack: stack });
    }

    protected $_firstBuyin(amount: number, stack: number, playerName: string, playerColor: string) {
        this.$store.dispatch(CashgameStoreActions.FirstBuyin, { amount: amount, stack: stack, name: playerName, color: playerColor });
    }

    protected $_hideForms() {
        this.$store.dispatch(CashgameStoreActions.HideForms);
    }

    protected $_cashout(stack: number) {
        this.$store.dispatch(CashgameStoreActions.Cashout, { stack: stack });
    }

    protected $_deleteAction(actionId: string) {
        this.$store.dispatch(CashgameStoreActions.DeleteAction, { id: actionId });
    }

    protected $_saveAction(data: object) {
        this.$store.dispatch(CashgameStoreActions.SaveAction, data);
    }

    protected $_selectPlayer(playerId: string) {
        this.$store.dispatch(CashgameStoreActions.SelectPlayer, { playerId });
    }

    protected $_report(stack: number) {
        this.$store.dispatch(CashgameStoreActions.Report, { stack });
    }

    $store: any;
}
