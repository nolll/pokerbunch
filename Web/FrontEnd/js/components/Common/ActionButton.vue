<template>
    <router-link :to="url" class="button button--action">{{text}}</router-link>
</template>

<script>
    export default {
        props: {
            url: {
                type: String
            },
            text: {
                type: String
            }
        }
    };
</script>

<style>

</style>
