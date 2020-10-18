<template>
    <Layout :ready="true">
        <PageSection>
            <Block>
                <PageHeading text="Page not found" />
            </Block>
            <Block>
                <p>
                    Please check the url for errors
                </p>
                <p>
                    Go to <CustomLink :url="homeUrl"></CustomLink>
                </p>
            </Block>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import urls from '@/urls';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { Component, Prop, Vue } from 'vue-property-decorator';

    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomLink
        }
    })
    export default class NotFoundPage extends Vue {
        get homeUrl(){
            return urls.home;
        }
    }
</script>
