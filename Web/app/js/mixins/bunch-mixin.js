import { BunchStoreGetters, BunchStoreActions } from '@/store/helpers/BunchStoreHelpers';

export default {
    computed: {
        $_bunchReady() {
            return this.$store.getters[BunchStoreGetters.BunchReady];
        },
        $_defaultBuyin() {
            return this.$store.getters[BunchStoreGetters.DefaultBuyin];
        },
        $_isManager() {
            return this.$store.getters[BunchStoreGetters.IsManager];
        },
        $_slug() {
            return this.$store.getters[BunchStoreGetters.Slug];
        },
        $_bunchName() {
            return this.$store.getters[BunchStoreGetters.Name];
        },
        $_userBunches() {
            return this.$store.getters[BunchStoreGetters.UserBunches];
        },
        $_userBunchesReady() {
            return this.$store.getters[BunchStoreGetters.UserBunchesReady];
        },
        $_description() {
            return this.$store.getters[BunchStoreGetters.Description];
        },
        $_houseRules() {
            return this.$store.getters[BunchStoreGetters.HouseRules];
        },
        $_currencyFormat() {
            return this.$store.getters[BunchStoreGetters.CurrencyFormat];
        },
        $_thousandSeparator() {
            return this.$store.getters[BunchStoreGetters.ThousandSeparator];
        }
    },
    methods: {
        $_loadBunch() {
            this.$store.dispatch(BunchStoreActions.LoadBunch, { slug: this.$route.params.slug });
        },
        $_loadUserBunches() {
            this.$store.dispatch(BunchStoreActions.LoadUserBunches);
        }
    }
};
