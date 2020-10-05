<template>
    <router-link :to="checkedUrl" :class="cssClasses" v-if="isInRouter"><slot></slot></router-link>
    <a :href="checkedUrl" :class="cssClasses" v-else><slot></slot></a>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import { CssClasses } from '@/models/CssClasses';

    @Component
    export default class CustomLink extends Vue {
        @Prop() readonly url!: string;
        @Prop({default: () => {}}) readonly cssClasses!: CssClasses;

        get isInRouter() {
            if (!this.url)
                return false;
            let resolved = this.$router.resolve(this.url);
            return resolved && resolved.route.name != '404';
        }
            
        get checkedUrl() {
            return this.url || '#'
        }
    }
</script>
