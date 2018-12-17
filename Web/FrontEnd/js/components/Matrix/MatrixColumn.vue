<template>
    <th class="table-list__column-header">
        <a :href="url" class="table-list__column-header__content">{{displayDate}}</a>
    </th>
</template>

<script>
    import moment from 'moment';

    export default {
        props: ['game'],
        computed: {
            url() {
                return '/cashgame/details/' + this.game.id;
            },
            displayDate() {
                return moment(this.game.startTime).format('MMM D');
            },
        }
    };
</script>

<style>

</style>
