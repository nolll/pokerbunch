<template>
    <CustomLink :url="url">{{name}}</CustomLink>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { LocationResponse } from '@/response/LocationResponse'

    @Component({
        components: {
            CustomLink
        }
    })
    export default class LocationListItem extends Vue {
        @Prop() readonly location!: LocationResponse;
        @Prop() readonly bunchId!: string;

        get name(){
            return this.location.name;
        }

        get url() {
            return urls.location.details(this.bunchId, this.location.id.toString());
        }
    }
</script>
