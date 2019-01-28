<template>
    <two-column :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <template slot="main">
            <page-section>
                <page-heading :text="name" />
            </page-section>

            <page-section v-if="hasDescription">
                <p>
                    {{description}}
                </p>
            </page-section>

            <page-section v-if="hasHouseRules">
                <h2>House Rules</h2>
                <p>
                    {{houseRules}}
                </p>
            </page-section>

            <page-section v-if="isManager">
                <p>
                    <custom-link :url="editUrl">Edit Bunch</custom-link>
                </p>
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import urls from '@/urls';
    import { TwoColumn } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { BUNCH } from '@/store-names';
    import { PageHeading, PageSection } from '@/components/Common';
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        components: {
            TwoColumn,
            BunchNavigation,
            PageHeading,
            PageSection,
            CustomLink
        },
        mixins: [
            DataMixin
        ],
        computed: {
            ...mapGetters(BUNCH, {
                slug: 'slug',
                name: 'name',
                description: 'description',
                houseRules: 'houseRules',
                isManager: 'isManager'
            }),
            hasDescription() {
                return !!this.description;
            },
            hasHouseRules() {
                return !!this.houseRules;
            },
            canEdit() {
                return this.isManager;
            },
            editUrl() {
                return urls.bunch.edit(this.slug);
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
        mounted: function () {
            this.init();
        }
    };
</script>

<style>

</style>
