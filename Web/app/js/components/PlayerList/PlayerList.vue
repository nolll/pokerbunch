<template>
    <SimpleList>
        <SimpleListItem v-for="player in players" :key="player.id">
            <PlayerList-item :id="player.id" :name="player.name" :color="player.color"/>
        </SimpleListItem>
    </SimpleList>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import PlayerListItem from '@/components/PlayerList/PlayerListItem.vue';
    import { PlayerMixin } from '@/mixins';

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            PlayerListItem
        }
    })
    export default class PlayerList extends Mixins(
        PlayerMixin
    ) {
        get players(){
            return this.$_players;
        }

        get ready() {
            return this.$_playersReady;
        }
    }
</script>
