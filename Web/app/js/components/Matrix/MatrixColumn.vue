<template>
    <th class="table-list__column-header">
        <CustomLink :url="url" cssClasses="table-list__column-header__content">{{displayDate}}</CustomLink>
    </th>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import moment from 'moment';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import urls from '@/urls';
    import { ArchiveCashgame } from '@/models/ArchiveCashgame';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class MatrixColumn extends Vue {
        @Prop() readonly game!: ArchiveCashgame;
        @Prop() readonly slug!: string;

        get url() {
            return urls.cashgame.details(this.slug, this.game.id);
        }

        get displayDate() {
            return moment(this.game.startTime).format('MMM D');
        }
    }
</script>
