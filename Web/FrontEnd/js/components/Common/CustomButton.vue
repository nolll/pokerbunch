<template>
    <custom-link :url="url" :cssClasses="cssClasses" v-if="hasUrl">{{text}}</custom-link>
    <button v-on:click="click" :class="cssClasses" v-else>{{text}}</button>
</template>

<script>
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        components: {
            CustomLink
        },
        props: {
            type: {
                type: String
            },
            url: {
                type: String
            },
            text: {
                type: String
            }
        },
        computed: {
            hasUrl() {
                return !!this.url;
            },
            cssClasses() {
                return {
                    'button': true,
                    'button--action': this.type === 'action'
                }
            }
        },
        methods: {
            click() {
                this.$emit('click');
            }
        }
    };
</script>

<style>

</style>
