<template>
    <SimpleList>
        <SimpleListItem v-for="event in events" :key="event.id">
            <EventListItem :event="event" />
        </SimpleListItem>
    </SimpleList>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import EventListItem from '@/components/EventList/EventListItem.vue';
    import { EventMixin } from '@/mixins';

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            EventListItem
        }
    })
    export default class EventList extends Mixins(
        EventMixin
    ) {
        get events(){
            return this.$_events;
        }

        get ready() {
            return this.$_eventsReady;
        }
    }
</script>
