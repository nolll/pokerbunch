<template>
    <layout :ready="ready">
        <template slot="top-nav">
            <bunch-navigation />
        </template>

        <page-section>
            <block>
                <page-heading :text="$_bunchName" />
            </block>

            <block v-if="hasDescription">
                {{$_description}}
            </block>

            <block v-if="hasHouseRules">
                <h2>House Rules</h2>
                <p>
                    {{$_houseRules}}
                </p>
            </block>

            <block v-if="$_isManager">
                <custom-link :url="editUrl">Edit Bunch</custom-link>
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { BunchMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import { Layout } from '@/components/Layouts';
    import { BunchNavigation } from '@/components/Navigation';
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
            BunchMixin,
            UserMixin
        ],
        computed: {
            hasDescription() {
                return !!this.$_description;
            },
            hasHouseRules() {
                return !!this.$_houseRules;
            },
            canEdit() {
                return this.$_isManager;
            },
            editUrl() {
                return urls.bunch.edit(this.$_slug);
            },
            ready() {
                return this.$_bunchReady;
            }
        },
        methods: {
            init() {
                this.$_requireUser();
                this.$_loadBunch();
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
