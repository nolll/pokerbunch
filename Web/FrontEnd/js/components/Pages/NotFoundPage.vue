<template>
    <two-column>
        <template slot="main">
            <div class="block gutter">
                <page-heading text="Page not found" />
            </div>

            <div class="block gutter">
                <p>
                    Please check the url for errors
                </p>
            </div>
        </template>
    </two-column>
</template>

<script>
    import { TwoColumn } from '@/components/Layouts';
    import { PageHeading } from '@/components/Common';

    export default {
        components: {
            TwoColumn,
            PageHeading
        }
    };
</script>

<style>
</style>
