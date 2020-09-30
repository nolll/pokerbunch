<template>
    <CustomLink :url="url">{{name}}</CustomLink>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class UserBunchListItem extends Vue {
        @Prop() readonly id!: string;
        @Prop() readonly name!: string;

        get url() {
            return urls.bunch.details(this.id);
        }
    }
</script>
