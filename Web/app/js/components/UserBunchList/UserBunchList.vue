<template>
    <div v-if="ready && hasBunches">
        <h1 class="module-heading">Your Bunches</h1>
        <SimpleList>
            <SimpleListItem v-for="bunch in userBunches" :key="bunch.id">
                <UserBunchList-item :id="bunch.id" :name="bunch.name" />
            </SimpleListItem>
        </SimpleList>
    </div>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import UserBunchListItem from '@/components/UserBunchList/UserBunchListItem.vue';
    import { BunchMixin } from '@/mixins';

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            UserBunchListItem
        }
    })
    export default class UserBunchList extends Mixins(
        BunchMixin
    ) {
        get userBunches(){
            return this.$_userBunches;
        }

        get hasBunches() {
            return this.$_userBunches.length > 0;
        }

        get ready() {
            return this.$_userBunchesReady;
        }
    }
</script>
