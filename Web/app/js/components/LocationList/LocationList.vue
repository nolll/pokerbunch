<template>
    <SimpleList>
        <SimpleListItem v-for="location in locations" :key="location.id">
            <LocationListItem :bunch-id="bunchId" :location="location" />
        </SimpleListItem>
    </SimpleList>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import LocationListItem from '@/components/LocationList/LocationListItem.vue';
    import { BunchMixin, LocationMixin } from '@/mixins';
    import comparer from '@/comparer';
import { LocationResponse } from '@/response/LocationResponse';

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            LocationListItem
        }
    })
    export default class LocationList extends Mixins(
        BunchMixin,
        LocationMixin
    ) {
        get bunchId(){
            return this.$_slug;
        }

        get locations(){
            return this.$_locations.slice().sort(compareBuyin);
        }

        get ready() {
            return this.$_locationsReady;
        }
    }

    function compareBuyin(a: LocationResponse, b: LocationResponse) {
        return comparer.compare(a.name, b.name);
    }
</script>
