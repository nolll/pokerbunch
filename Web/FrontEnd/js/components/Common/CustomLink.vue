<template>
    <router-link :to="checkedUrl" :class="cssClasses" v-if="isInRouter"><slot></slot></router-link>
    <a :href="checkedUrl" :class="cssClasses" v-else><slot></slot></a>
</template>

<script>
    export default {
        props: {
            url: {
                type: String
            },
            cssClasses: {

            }
        },
        computed: {
            isInRouter() {
                if (!this.url)
                    return false;
                let resolved = this.$router.resolve(this.url);
                return resolved && resolved.routeName && resolved.routeName != '404';
            },
            checkedUrl() {
                return this.url || '#'
            }
        }
    };
</script>

<style>

</style>
