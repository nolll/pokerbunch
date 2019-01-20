<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="aside">
            <div class="block gutter">
                <custom-button :url="addPlayerUrl" type="action" text="Add player"/>
            </div>
        </template>

        <template slot="main">
            <div class="block gutter">
                <player-list />
            </div>
        </template>
    </two-column>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { PlayerList } from '@/components';
    import CustomButton from '@/components/common/CustomButton.vue';
    import urls from '@/urls';
    import { BUNCH } from '@/store-names';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            PlayerList,
            CustomButton
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ...mapGetters(BUNCH, [
                'slug'
            ]),
            addPlayerUrl() {
                return urls.addPlayer(this.slug);
            },
            ready() {
                return this.bunchReady;
            }
        },
        methods: {
            init() {
                this.loadUser();
                this.loadBunch();
            }
        },
        watch: {
            '$route'(to, from) {
                this.init();
            }
        },
        created: function () {
            this.init();
        }
    };
</script>

<style>

</style>
