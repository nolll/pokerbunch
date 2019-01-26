<template>
    <router-link :to="url" :class="cssClasses" v-if="isInRouter"><slot></slot></router-link>
    <a :href="url" :class="cssClasses" v-else><slot></slot></a>
</template>

<script>
    export default {
        props: {
            url: {
                type: String
            },
            text: {
                type: String
            },
            cssClasses: {

            }
        },
        computed: {
            isInRouter() {
                let resolved = this.$router.resolve(this.url);
                var routeName = resolved.route.name;
                return routeName != '404';
            }
        }
    };
</script>

<style>

</style>
