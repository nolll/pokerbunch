<template>
    <simple-list>
        <simple-list-item v-for="player in $_players" :key="player.id">
            <player-list-item :id="player.id" :name="player.name" :color="player.color"/>
        </simple-list-item>
    </simple-list>
</template>

<script>
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import PlayerListItem from '@/components/PlayerList/PlayerListItem.vue';
    import { PlayerMixin } from '@/mixins';

    export default {
        components: {
            SimpleList,
            SimpleListItem,
            PlayerListItem
        },
        mixins: [
            PlayerMixin
        ],
        computed: {
            ready() {
                return this.$_playersReady;
            }
        }
    };
</script>

<style>

</style>
