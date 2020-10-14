import { Component, Vue } from 'vue-property-decorator';
import { BunchStoreGetters, BunchStoreActions } from '@/store/helpers/BunchStoreHelpers';
import { BunchResponse } from '@/response/BunchResponse';

@Component
export class BunchMixin extends Vue {
    protected get $_bunchReady(): boolean {
        return this.$store.getters[BunchStoreGetters.BunchReady];
    }

    protected get $_defaultBuyin(): number {
        return this.$store.getters[BunchStoreGetters.DefaultBuyin];
    }

    protected get $_isManager(): boolean {
        return this.$store.getters[BunchStoreGetters.IsManager];
    }

    protected get $_slug(): string {
        return this.$store.getters[BunchStoreGetters.Slug];
    }

    protected get $_bunchName(): string {
        return this.$store.getters[BunchStoreGetters.Name];
    }

    protected get $_userBunches(): BunchResponse[] {
        return this.$store.getters[BunchStoreGetters.UserBunches];
    }

    protected get $_userBunchesReady(): boolean {
        return this.$store.getters[BunchStoreGetters.UserBunchesReady];
    }

    protected get $_bunches(): BunchResponse[] {
        return this.$store.getters[BunchStoreGetters.Bunches];
    }

    protected get $_bunchesReady(): boolean {
        return this.$store.getters[BunchStoreGetters.BunchesReady];
    }

    protected get $_description(): string {
        return this.$store.getters[BunchStoreGetters.Description];
    }

    protected get $_houseRules(): string {
        return this.$store.getters[BunchStoreGetters.HouseRules];
    }

    protected get $_currencyFormat(): string {
        return this.$store.getters[BunchStoreGetters.CurrencyFormat];
    }

    protected get $_thousandSeparator(): string {
        return this.$store.getters[BunchStoreGetters.ThousandSeparator];
    }

    protected $_loadBunch() {
        this.$store.dispatch(BunchStoreActions.LoadBunch, { slug: this.$route.params.slug });
    }

    protected $_loadUserBunches() {
        this.$store.dispatch(BunchStoreActions.LoadUserBunches);
    }

    protected $_loadBunches() {
        this.$store.dispatch(BunchStoreActions.LoadBunches);
    }

    $store: any;
}
