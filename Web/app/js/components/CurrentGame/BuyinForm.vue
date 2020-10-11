<template>
    <div class="form">
        <div class="field">
            <label class="label" for="buyin-amount">Amount</label>
            <input class="numberfield" v-model="amount" v-on:focus="focus" ref="buyin" id="buyin-amount" type="text" pattern="[0-9]*">
        </div>
        <div class="field" v-if="$_isInGame">
            <label class="label" for="buyin-stack">Stack Size</label>
            <input class="numberfield" v-model="stack" v-on:focus="focus" id="buyin-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <CustomButton v-on:click="buyin" type="action" text="Buy In" />
            <CustomButton v-on:click="cancel" text="Cancel" />
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Mixins, Watch } from 'vue-property-decorator';
    import validate from '@/validate';
    import forms from '@/forms';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import { BunchMixin, CashgameMixin, PlayerMixin } from '@/mixins';

    @Component({
        components: {
            CustomButton
        }
    })
    export default class BuyinForm extends Mixins(
        BunchMixin,
        CashgameMixin,
        PlayerMixin
    ) {
        @Prop() readonly isActive!: boolean;

        amount = 0;
        stack = 0;
        buyinError: string | null = null;
        stackError: string | null = null;

        get currentPlayer() {
            return this.$_getPlayer(this.$_playerId);
        }

        get playerName() {
            if (!this.currentPlayer)
                return '';
            return this.currentPlayer.name;
        }

        get playerColor() {
            if (!this.currentPlayer)
                return '';
            return this.currentPlayer.color;
        }

        get hasErrors() {
            return this.buyinError === null && this.stackError === null;
        }

        buyin() {
            this.validateForm();
            if (!this.hasErrors) {
                if (this.$_isInGame) {
                    this.$_buyin(this.amount, this.stack);
                } else {
                    this.$_firstBuyin(this.amount, this.stack, this.playerName, this.playerColor);
                }
            }
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
            this.amount = this.$_defaultBuyin;
        }
    }
</script>
