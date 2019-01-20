<template>
    <simple-list>
        <simple-list-item v-for="player in players" :key="player.id">
            <player-list-item :id="player.id" :name="player.name" :color="player.color"/>
        </simple-list-item>
    </simple-list>
</template>

<script>
    import { mapGetters } from 'vuex';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import PlayerListItem from '@/components/PlayerList/PlayerListItem.vue';
    import { BUNCH } from '@/store-names';

    export default {
        components: {
            SimpleList,
            SimpleListItem,
            PlayerListItem
        },
        computed: {
            ...mapGetters(BUNCH, [
                'bunchReady',
                'players'
            ]),
            ready() {
                return this.bunchReady;
            }
        }
    };
</script>

<style>

</style>
