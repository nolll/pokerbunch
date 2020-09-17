<template>
    <div v-if="ready && hasBunches">
        <h1 class="module-heading">Your Bunches</h1>
        <simple-list>
            <simple-list-item v-for="bunch in $_userBunches" :key="bunch.id">
                <user-bunch-list-item :id="bunch.id" :name="bunch.name" />
            </simple-list-item>
        </simple-list>
    </div>
</template>

<script>
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import UserBunchListItem from '@/components/UserBunchList/UserBunchListItem.vue';
    import { BunchMixin } from '@/mixins';

    export default {
        components: {
            SimpleList,
            SimpleListItem,
            UserBunchListItem
        },
        mixins: [
            BunchMixin
        ],
        computed: {
            hasBunches() {
                return this.$_userBunches.length > 0;
            },
            ready() {
                return this.$_userBunchesReady;
            }
        }
    };
</script>

<style>

</style>
