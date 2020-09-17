import { CashgameStoreGetters, CashgameStoreActions } from '@/store/helpers/CashgameStoreHelpers';

export default {
    computed: {
        $_isInGame() {
            return this.$store.getters[CashgameStoreGetters.IsInGame];
        },
        $_playerId() {
            return this.$store.getters[CashgameStoreGetters.PlayerId];
        },
        $_cashgameId() {
            return this.$store.getters[CashgameStoreGetters.CashgameId];
        },
        $_totalBuyin() {
            return this.$store.getters[CashgameStoreGetters.TotalBuyin];
        },
        $_totalStacks() {
            return this.$store.getters[CashgameStoreGetters.TotalStacks];
        },
        $_isRunning() {
            return this.$store.getters[CashgameStoreGetters.IsRunning];
        },
        $_locationId() {
            return this.$store.getters[CashgameStoreGetters.LocationId];
        },
        $_locationName() {
            return this.$store.getters[CashgameStoreGetters.LocationName];
        },
        $_reportFormVisible() {
            return this.$store.getters[CashgameStoreGetters.ReportFormVisible];
        },
        $_buyinFormVisible() {
            return this.$store.getters[CashgameStoreGetters.BuyinFormVisible];
        },
        $_cashoutFormVisible() {
            return this.$store.getters[CashgameStoreGetters.CashoutFormVisible];
        },
        $_cashgamePlayers() {
            return this.$store.getters[CashgameStoreGetters.Players];
        },
        $_hasPlayers() {
            return this.$store.getters[CashgameStoreGetters.HasPlayers];
        },
        $_startTime() {
            return this.$store.getters[CashgameStoreGetters.StartTime];
        },
        $_updatedTime() {
            return this.$store.getters[CashgameStoreGetters.UpdatedTime];
        },
        $_sortedPlayers() {
            return this.$store.getters[CashgameStoreGetters.SortedPlayers];
        },
        $_canCashout() {
            return this.$store.getters[CashgameStoreGetters.CanCashout];
        },
        $_canReport() {
            return this.$store.getters[CashgameStoreGetters.CanReport];
        },
        $_canBuyin() {
            return this.$store.getters[CashgameStoreGetters.CanBuyin];
        },
        $_cashgameReady() {
            return this.$store.getters[CashgameStoreGetters.CashgameReady];
        }
    },
    methods: {
        $_loadCashgame() {
            this.$store.dispatch(CashgameStoreActions.LoadCashgame, { id: this.$route.params.id });
        },
        $_showReportForm() {
            this.$store.dispatch(CashgameStoreActions.ShowReportForm);
        },
        $_showBuyinForm() {
            this.$store.dispatch(CashgameStoreActions.ShowBuyinForm);
        },
        $_showCashoutForm() {
            this.$store.dispatch(CashgameStoreActions.ShowCashoutForm);
        },
        $_buyin(amount, stack) {
            this.$store.dispatch(CashgameStoreActions.Buyin, { amount: amount, stack: stack });
        },
        $_firstBuyin(amount, stack, playerName, playerColor) {
            this.$store.dispatch(CashgameStoreActions.FirstBuyin, { amount: amount, stack: stack, name: playerName, color: playerColor });
        },
        $_hideForms() {
            this.$store.dispatch(CashgameStoreActions.HideForms);
        },
        $_cashout(stack) {
            this.$store.dispatch(CashgameStoreActions.Cashout, { stack: stack });
        },
        $_deleteAction(actionId) {
            this.$store.dispatch(CashgameStoreActions.DeleteAction, { id: actionId });
        },
        $_saveAction(data) {
            this.$store.dispatch(CashgameStoreActions.SaveAction, data);
        },
        $_selectPlayer(playerId) {
            this.$store.dispatch(CashgameStoreActions.SelectPlayer, { playerId: playerId });
        },
        $_report(stack) {
            this.$store.dispatch(CashgameStoreActions.Report, { stack: stack });
        }
    }
};
