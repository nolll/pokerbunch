<template>
    <div class="form">
        <div class="field">
            <label class="label" for="buyin-amount">Amount</label>
            <input class="numberfield" v-model.number="amount" v-on:focus="focus" ref="buyin" id="buyin-amount" type="text" pattern="[0-9]*">
        </div>
        <div class="field" v-if="isPlayerInGame">
            <label class="label" for="buyin-stack">Stack Size</label>
            <input class="numberfield" v-model.number="stack" v-on:focus="focus" id="buyin-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <CustomButton v-on:click="buyin" type="action" text="Buy In" />
            <CustomButton v-on:click="cancel" text="Cancel" />
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import validate from '@/validate';
    import forms from '@/forms';
    import CustomButton from '@/components/Common/CustomButton.vue';

    @Component({
        components: {
            CustomButton
        }
    })
    export default class BuyinForm extends Vue {
        @Prop() readonly defaultBuyin!: number;
        @Prop() readonly isPlayerInGame!: boolean;

        amount = 0;
        stack = 0;
        buyinError: string | null = null;
        stackError: string | null = null;

        get hasErrors() {
            return this.buyinError === null && this.stackError === null;
        }

        buyin() {
            this.validateForm();
            if (!this.hasErrors) {
                this.$emit('buyin', this.amount, this.stack);
            }
        }

        cancel() {
            this.$emit('cancel');
        }

        focus(e: FocusEvent) {
            if(e.target)
                forms.selectAll(e.target as HTMLInputElement);
        }

        validateForm() {
            this.clearErrors();
            if (validate.intRange(this.amount, 1))
                this.buyinError = 'Buyin must be greater than zero';
            if (validate.intRange(this.stack, 0))
                this.stackError = 'Stack can\'t be negative';
        }

        clearErrors() {
            this.buyinError = null;
            this.stackError = null;
        }

        mounted() {
            this.amount = this.defaultBuyin;
        }
    }
</script>
