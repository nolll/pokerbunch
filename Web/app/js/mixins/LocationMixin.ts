import { Component, Vue } from 'vue-property-decorator';
import { LocationStoreGetters, LocationStoreActions } from '@/store/helpers/LocationStoreHelpers';
import { LocationResponse } from '@/response/LocationResponse';

@Component
export class LocationMixin extends Vue {
    protected get $_locationsReady() : boolean {
        return this.$store.getters[LocationStoreGetters.LocationsReady];
    }

    protected get $_locations(): LocationResponse[] {
        return this.$store.getters[LocationStoreGetters.Locations];
    }

    protected $_getLocation(id: string): LocationResponse | null {
        return this.$_locations.find(l => l.id.toString() === id) || null;
    }

    protected $_loadLocations() {
        this.$store.dispatch(LocationStoreActions.LoadLocations, { slug: this.$route.params.slug });
    }

    protected $_addLocation(name: string) {
        this.$store.dispatch(LocationStoreActions.AddLocation, { bunchId: this.$route.params.slug, name });
    }

    $store: any;
}
