<template>
    <th class="table-list__column-header">
        <a :href="url" class="table-list__column-header__content">{{displayDate}}</a>
    </th>
</template>

<script>
    import { mapState } from 'vuex';
    import moment from 'moment';

    export default {
        props: ['game'],
        computed: {
            url: function () {
                return '/cashgame/details/' + this.game.id;
            },
            displayDate: function () {
                return moment(this.game.startTime).format('MMM D');
            },
        }
    };
</script>

<style>

</style>
