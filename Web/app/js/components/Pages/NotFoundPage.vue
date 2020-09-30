<template>
    <Layout :ready="true">
        <PageSection>
            <Block>
                <PageHeading text="Page not found" />
            </Block>
            <Block>
                Please check the url for errors
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection
        }
    })
    export default class NotFoundPage extends Vue {
    }
</script>
