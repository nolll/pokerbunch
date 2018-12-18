<template>
    <two-column>
        <template slot="main">
            <div class="block gutter">
                <h1 class="page-heading">Page not found</h1>
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

    export default {
        components: {
            TwoColumn
        }
    };
</script>

<style>
</style>
