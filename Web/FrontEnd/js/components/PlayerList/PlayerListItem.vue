<template>
    <div>
        <span class="player-color-box" :style="colorBoxStyle"></span>
        <router-link :to="url">{{name}}</router-link>
    </div>
</template>

<script>
    import urls from '@/urls';

    export default {
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
                return urls.playerDetails(this.id);
            },
            colorBoxStyle() {
                return `background-color: ${this.color}`;
            }
        }
    };
</script>

<style>

</style>
