<template>
    <a :href="url" :class="cssClasses" v-if="hasUrl">{{text}}</a>
    <button v-on:click="click" :class="cssClasses" v-else>{{text}}</button>
</template>

<script>
    export default {
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
