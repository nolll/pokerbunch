<template>
    <div>
        <custom-link :url="url">{{userName}}</custom-link>
    </div>
</template>

<script>
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        props: {
            userName: {
                type: String
            }
        },
        computed: {
            url() {
                return urls.userDetails(this.userName);
            }
        }
    };
</script>

<style>

</style>
