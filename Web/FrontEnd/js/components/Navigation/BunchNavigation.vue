<template>
    <nav class="game-nav">
        <h2><a :href="bunchDetailsUrl">{{name}}</a></h2>
        <ul>
            <li><a :href="cashgamesUrl"><span>Cashgames</span></a></li>
            <li><a :href="playersUrl"><span>Players</span></a></li>
            <li><a :href="eventsUrl"><span>Events</span></a></li>
            <li><a :href="locationsUrl"><span>Locations</span></a></li>
        </ul>
    </nav>
</template>

<script>
    import { mapState } from 'vuex';

    export default {
        computed: {
            ...mapState('bunch', {
                slug: state => state.slug,
                name: state => state.name
            }),
            bunchDetailsUrl() {
                return '/bunch/details/' + this.slug;
            },
            cashgamesUrl() {
                return '/cashgame/index/' + this.slug;
            },
            playersUrl() {
                return '/player/list/' + this.slug;
            },
            eventsUrl() {
                return '/event/list/' + this.slug;
            },
            locationsUrl() {
                return '/location/list/' + this.slug;
            }
        }
    };
</script>

<style>
</style>
