<template>
    <TableListColumnHeader>
        {{year}}
    </TableListColumnHeader>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import TableListColumnHeader from '@/components/Common/TableList/TableListColumnHeader.vue';

    @Component({
        components: {
            TableListColumnHeader
        }
    })
    export default class YearMatrixColumn extends Vue {
        @Prop() readonly year!: number;
    }
</script>
