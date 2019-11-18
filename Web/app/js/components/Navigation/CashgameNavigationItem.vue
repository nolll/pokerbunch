<template>
    <li><router-link :to="url" :class="selectedClass" @click.native="click">{{text}}</router-link></li>
</template>

<script>
    export default {
        props: ['url', 'text', 'isSelected'],
        computed: {
            selectedClass() {
                if (this.isSelected) {
                    return 'selected';
                }
                return '';
            }
        },
        methods: {
            click() {
                this.$emit('selected');
            }
        }
    };
</script>

<style lang="less" scoped>
    li{
        margin-top: 5px;
    }
</style>
