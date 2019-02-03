<template>
    <layout :ready="true">
        <page-section>
            <block>
                <page-heading text="Page not found" />
            </block>
            <block>
                Please check the url for errors
            </block>
        </page-section>
    </layout>
</template>

<script>
    import { Layout } from '@/components/Layouts';
    import { Block, PageHeading, PageSection } from '@/components/Common';

    export default {
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection
        }
    };
</script>

<style>
</style>
