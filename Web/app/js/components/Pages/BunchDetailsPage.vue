<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <page-heading :text="name" />
            </block>

            <block v-if="hasDescription">
                {{description}}
            </block>

            <block v-if="hasHouseRules">
                <h2>House Rules</h2>
                <p>
                    {{houseRules}}
                </p>
            </block>

            <block v-if="isManager">
                <custom-link :url="editUrl">Edit Bunch</custom-link>
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { DataMixin } from '@/mixins';
    import urls from '@/urls';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
    import { BUNCH } from '@/store-names';
    import { Block, PageHeading, PageSection } from '@/components/Common';
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        components: {
            Layout,
            BunchNavigation,
            Block,
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
                this.requireUser();
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
