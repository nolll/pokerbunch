<template>
    <div>
        <CustomLink :url="url">{{name}}</CustomLink>,
        {{details}}
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { EventResponse } from '@/response/EventResponse'

    @Component({
        components: {
            CustomLink
        }
    })
    export default class EventListItem extends Vue {
        @Prop() readonly event!: EventResponse;

        get name(){
            return this.event.name;
        }

        get location(){
            return this.event.location.name;
        }

        get date(){
            return this.event.startDate;
        }

        get url() {
            return urls.event.details(this.event.bunchId, this.event.id.toString());
        }

        get details(){
            return this.hasGames
                ? `${this.location}, ${this.date}`
                : 'No games';
        }

        get hasGames(){
            return !!this.event.location && !!this.event.startDate;
        }
    }
</script>
