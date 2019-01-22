<template>
    <div>
        <a :href="url">{{userName}}</a>
    </div>
</template>

<script>
    import urls from '@/urls';

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
