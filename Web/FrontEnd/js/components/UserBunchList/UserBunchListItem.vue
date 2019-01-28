<template>
    <custom-link :url="url">{{name}}</custom-link>
</template>

<script>
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        components: {
            CustomLink
        },
        props: {
            id: {
                type: Number
            },
            name: {
                type: String
            }
        },
        computed: {
            url() {
                return urls.bunch.details(this.id);
            }
        }
    };
</script>

<style>

</style>
