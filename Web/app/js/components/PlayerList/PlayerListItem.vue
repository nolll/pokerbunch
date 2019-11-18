<template>
    <div>
        <span class="player-color-box" :style="colorBoxStyle"></span>
        <custom-link :url="url">{{name}}</custom-link>
    </div>
</template>

<script>
    import urls from '@/urls';
    import CustomLink from '@/components/Common/CustomLink.vue';

    export default {
        components: {
            CustomLink
        },
        props: {
            id: {
                type: Number
            },
            name: {
                type: String
            },
            color: {
                type: String
            }
        },
        computed: {
            url() {
                return urls.player.details(this.id);
            },
            colorBoxStyle() {
                return `background-color: ${this.color}`;
            }
        }
    };
</script>

<style>

</style>
