import { Component, Vue } from 'vue-property-decorator';
import { EventStoreGetters, EventStoreActions } from '@/store/helpers/EventStoreHelpers';
import { EventResponse } from '@/response/EventResponse';

@Component
export class EventMixin extends Vue {
    protected get $_eventsReady() : boolean {
        return this.$store.getters[EventStoreGetters.EventsReady];
    }

    protected get $_events(): EventResponse[] {
        return this.$store.getters[EventStoreGetters.Events];
    }

    protected $_getEvent(id: string): EventResponse | null {
        return this.$_events.find(e => e.id.toString() === id) || null;
    }

    protected $_loadEvents() {
        this.$store.dispatch(EventStoreActions.LoadEvents, { slug: this.$route.params.slug });
    }

    protected $_addEvent(name: string) {
        this.$store.dispatch(EventStoreActions.AddEvent, { bunchId: this.$route.params.slug, name });
    }

    $store: any;
}
