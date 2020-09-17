<template>
    <nav class="game-nav" v-if="hasSlug">
        <h2><custom-link :url="bunchDetailsUrl">{{bunchName}}</custom-link></h2>
        <ul>
            <li><custom-link :url="cashgamesUrl"><span>Cashgames</span></custom-link></li>
            <li><custom-link :url="playersUrl"><span>Players</span></custom-link></li>
            <li><custom-link :url="eventsUrl"><span>Events</span></custom-link></li>
            <li><custom-link :url="locationsUrl"><span>Locations</span></custom-link></li>
        </ul>
    </nav>
</template>

<script>
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { BunchMixin } from '@/mixins';

    export default {
        components: {
            CustomLink
        },
        mixins: [
            BunchMixin
        ],
        computed: {
            slug() {
                if (this.$_slug && this.$_slug.length > 0)
                    return this.$_slug;
                if (this.$_userBunches.length > 0)
                    return this.$_userBunches[0].id;
                return null;
            },
            bunchName() {
                if (this.$_bunchName && this.$_bunchName.length > 0)
                    return this.$_bunchName;
                if (this.$_userBunches.length > 0)
                    return this.$_userBunches[0].name;
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
