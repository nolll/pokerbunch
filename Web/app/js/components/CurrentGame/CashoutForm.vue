<template>
    <div class="form">
        <div class="field">
            <label class="label" for="cashout-stack">Stack Size</label>
            <input class="numberfield" v-model.number="stack" v-on:focus="focus" ref="stack" id="cashout-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <CustomButton v-on:click="cashout" type="action" text="Cash Out" />
            <CustomButton v-on:click="cancel" text="Cancel" />
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins, Watch } from 'vue-property-decorator';
    import validate from '@/validate';
    import forms from '@/forms';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import Spinner from '@/components/Common/Spinner.vue';
    import { BunchMixin, CashgameMixin } from '@/mixins';

    @Component({
        components: {
            CustomButton
        }
    })
    export default class CashoutForm extends Mixins(
        BunchMixin,
        CashgameMixin
    ) {
        @Prop() readonly isActive!: boolean;

        stack: number = 0;
        stackError: string | null = null;

        get hasErrors() {
            return this.stackError === null;
        }

        cashout() {
            this.validateForm();
            if (!this.hasErrors)
                this.$_cashout(this.stack);
        }

        cancel() {
            this.$_hideForms();
        }

        focus(e: FocusEvent) {
            if(e.target)
                forms.selectAll(e.target as HTMLInputElement);
        }

        validateForm() {
            this.clearErrors();
            if (validate.intRange(this.stack, 0))
                this.stackError = 'Stack can\'t be negative';
        }

        clearErrors() {
            this.stackError = null;
        }

        mounted() {
            this.stack = this.$_defaultBuyin;
        }

        @Watch('isActive')
        isActiveChanged(val: boolean) {
            if (val) {
                var el = this.$refs.buyin as HTMLInputElement;
                el.focus();
            }
        }
    }
</script>
