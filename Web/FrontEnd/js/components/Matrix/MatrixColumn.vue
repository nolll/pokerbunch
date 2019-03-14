<template>
    <th class="table-list__column-header">
        <custom-link :url="url" cssClasses="table-list__column-header__content">{{displayDate}}</custom-link>
    </th>
</template>

<script>
    import moment from 'moment';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';

    export default {
        components: {
            CustomLink
        },
        props: ['game', 'slug'],
        computed: {
            url() {
                return urls.cashgame.details(this.slug, this.game.id);
            },
            displayDate() {
                return moment(this.game.startTime).format('MMM D');
            },
        }
    };
</script>

<style>

</style>
