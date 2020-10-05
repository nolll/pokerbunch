<template>
    <div class="matrix" v-if="ready">
        <TableList>
            <thead>
                <tr>
                    <TableListColumnHeader />
                    <TableListColumnHeader>Player</TableListColumnHeader>
                    <TableListColumnHeader>Winnings</TableListColumnHeader>
                    <MatrixColumn v-for="game in games" :game="game" :slug="slug" :key="game.id" />
                </tr>
            </thead>
            <tbody>
                <MatrixRow v-for="(player, index) in players" :player="player" :index="index" :key="player.id" />
            </tbody>
        </TableList>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import MatrixColumn from './MatrixColumn.vue';
    import MatrixRow from './MatrixRow.vue';
    import { BunchMixin, GameArchiveMixin } from '@/mixins';
    import TableList from '@/components/Common/TableList/TableList.vue';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

    @Component({
        components: {
            MatrixColumn,
            MatrixRow,
            TableList, 
            TableListColumnHeader
        }
    })
    export default class MatrixTable extends Mixins(
        BunchMixin,
        GameArchiveMixin
    ) {
        get slug(){
            return this.$_slug;
        }

        get games(){
            return this.$_sortedGames;
        }

        get players(){
            return this.$_sortedPlayers;
        }

        get ready() {
            return this.$_bunchReady && this.$_sortedGames.length > 0;
        }
    }
</script>
