<template>
    <two-column>
        <template slot="main">
            <page-section>
                <page-heading text="Page not found" />
            </page-section>

            <page-section>
                <p>
                    Please check the url for errors
                </p>
            </page-section>
        </template>
    </two-column>
</template>

<script>
    import { TwoColumn } from '@/components/Layouts';
    import { PageHeading, PageSection } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            PageHeading,
            PageSection
        }
    };
</script>

<style>
</style>
