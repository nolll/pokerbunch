<template>
    <CustomLink :url="url" :cssClasses="cssClasses" v-if="hasUrl">{{text}}</CustomLink>
    <button v-on:click="click" :class="cssClasses" v-else>{{text}}</button>
</template>

<script lang="ts">
    import { Component, Prop, Vue } from 'vue-property-decorator';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { ButtonType } from '@/models/ButtonType';
    import { CssClasses } from '@/models/CssClasses';

    @Component({
        components: {
            CustomLink
        }
    })
    export default class CustomButton extends Vue {
        @Prop({default: ButtonType.Default}) readonly type!: ButtonType;
        @Prop() readonly text!: string;
        @Prop() readonly url?: string | undefined;

        get hasUrl(): boolean {
            return !!this.url;
        }

        get cssClasses(): CssClasses {
            return {
                'button': true,
                'button--action': this.type === ButtonType.Action
            }
        }

        click() {
            this.$emit('click');
        }
    }
</script>
