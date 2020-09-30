<template>
    <div>
        <CustomLink :url="url">{{userName}}</CustomLink>
    </div>
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
    export default class UserListItem extends Vue {
        @Prop() readonly userName!: string;

        get url() {
            return urls.user.details(this.userName);
        }
    }
</script>
