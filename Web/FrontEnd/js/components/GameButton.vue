<template>
    <button class="button button--icon">
        <i v-bind:class="iconCssClass"></i>
        {{text}}
    </button>
</template>

<script>
    export default {
        props: ['text', 'icon'],
        computed: {
            iconCssClass: function () {
                return 'icon-' + this.icon;
            }
        }
    };
</script>

<style scoped>

</style>
