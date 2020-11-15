<template>
    <div>
        <span class="player-color-box" :style="colorBoxStyle"></span>
        <CustomLink :url="url">{{name}}</CustomLink>
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
    export default class PlayerListItem extends Vue {
        @Prop() readonly bunchId!: string;
        @Prop() readonly id!: number;
        @Prop() readonly name!: string;
        @Prop() readonly color!: string;

        get url() {
            return urls.player.details(this.bunchId, this.id.toString());
        }

        get colorBoxStyle() {
            return `background-color: ${this.color}`;
        }
    }
</script>
