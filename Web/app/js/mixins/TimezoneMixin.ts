import { Component, Vue } from 'vue-property-decorator';
import { TimezoneStoreGetters, TimezoneStoreActions } from '@/store/helpers/TimezoneStoreHelpers';
import { Timezone } from '@/models/Timezone';

@Component
export class TimezoneMixin extends Vue {
    protected get $_timezonesReady() : boolean {
        return this.$store.getters[TimezoneStoreGetters.TimezonesReady];
    }

    protected get $_timezones(): Timezone[] {
        return this.$store.getters[TimezoneStoreGetters.Timezones];
    }

    protected $_loadTimezones() {
        this.$store.dispatch(TimezoneStoreActions.LoadTimezones);
    }

    $store: any;
}
