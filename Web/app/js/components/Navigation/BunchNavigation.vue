<template>
    <nav class="game-nav" v-if="hasSlug">
        <h2><CustomLink :url="bunchDetailsUrl">{{bunchName}}</CustomLink></h2>
        <ul>
            <li><CustomLink :url="cashgamesUrl"><span>Cashgames</span></CustomLink></li>
            <li><CustomLink :url="playersUrl"><span>Players</span></CustomLink></li>
            <li><CustomLink :url="eventsUrl"><span>Events</span></CustomLink></li>
            <li><CustomLink :url="locationsUrl"><span>Locations</span></CustomLink></li>
        </ul>
    </nav>
</template>

<script lang="ts">
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { BunchMixin } from '@/mixins';

    import { Component, Mixins } from 'vue-property-decorator';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class BunchNavigation extends Mixins(
        BunchMixin
    ) {
        get slug() {
            if (this.$_slug && this.$_slug.length > 0)
                return this.$_slug;
            if (this.$_userBunches.length > 0)
                return this.$_userBunches[0].id;
            return '';
        }

        get bunchName() {
            if (this.$_bunchName && this.$_bunchName.length > 0)
                return this.$_bunchName;
            if (this.$_userBunches.length > 0)
                return this.$_userBunches[0].name;
            return '';
        }

        get hasSlug() {
            return !!this.slug;
        }

        get bunchDetailsUrl() {
            return urls.bunch.details(this.slug);
        }

        get cashgamesUrl() {
            return urls.cashgame.index(this.slug);
        }

        get playersUrl() {
            return urls.player.list(this.slug);
        }

        get eventsUrl() {
            return urls.event.list(this.slug);
        }

        get locationsUrl() {
            return urls.location.list(this.slug);
        }
    }
</script>
