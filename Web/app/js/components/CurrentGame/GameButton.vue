<template>
    <button class="button button--icon">
        <i :class="iconCssClass"></i>
        {{text}}
    </button>
</template>

<script>
    export default {
        props: {
            text: {
                type: String
            },
            icon: {
                type: String
            }
        },
        computed: {
            iconCssClass() {
                return `icon-${this.icon}`;
            }
        }
    };
</script>

<style>

</style>
