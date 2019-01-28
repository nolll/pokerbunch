<template>
    <nav class="game-nav" v-if="hasSlug">
        <h2><custom-link :url="bunchDetailsUrl">{{name}}</custom-link></h2>
        <ul>
            <li><custom-link :url="cashgamesUrl"><span>Cashgames</span></custom-link></li>
            <li><custom-link :url="playersUrl"><span>Players</span></custom-link></li>
            <li><custom-link :url="eventsUrl"><span>Events</span></custom-link></li>
            <li><custom-link :url="locationsUrl"><span>Locations</span></custom-link></li>
        </ul>
    </nav>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { BUNCH } from '@/store-names';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        components: {
            CustomLink
        },
        computed: {
            ...mapGetters(BUNCH, {
                selectedName: 'name',
                selectedSlug: 'slug',
                userBunches: 'userBunches'
            }),
            slug() {
                if (this.selectedSlug && this.selectedSlug.length > 0)
                    return this.selectedSlug;
                if (this.userBunches.length > 0)
                    return this.userBunches[0].id;
                return null;
            },
            name() {
                if (this.selectedName && this.selectedName.length > 0)
                    return this.selectedName;
                if (this.userBunches.length > 0)
                    return this.userBunches[0].name;
                return null;
            },
            hasSlug() {
                return !!this.slug;
            },
            bunchDetailsUrl() {
                return urls.bunch.details(this.slug);
            },
            cashgamesUrl() {
                return urls.cashgame.index(this.slug);
            },
            playersUrl() {
                return urls.player.list(this.slug);
            },
            eventsUrl() {
                return urls.event.list(this.slug);
            },
            locationsUrl() {
                return urls.location.list(this.slug);
            }
        }
    };
</script>

<style>
</style>
