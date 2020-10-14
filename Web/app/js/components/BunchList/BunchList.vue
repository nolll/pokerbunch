<template>
    <SimpleList>
        <SimpleListItem v-for="bunch in bunches" :key="bunch.id">
            <BunchListItem :id="bunch.id" :name="bunch.name" />
        </SimpleListItem>
    </SimpleList>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import BunchListItem from '@/components/BunchList/BunchListItem.vue';
    import { BunchMixin } from '@/mixins';

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            BunchListItem
        }
    })
    export default class BunchList extends Mixins(
        BunchMixin
    ) {
        get bunches(){
            return this.$_bunches;
        }
    }
</script>
