<template>
    <h1 class="page-heading">{{text}}</h1>
</template>

<script>
    export default {
        name: 'page-heading',
        props: {
            text: {
                type: String
            }
        }
    };
</script>

<style lang="less">
</style>
