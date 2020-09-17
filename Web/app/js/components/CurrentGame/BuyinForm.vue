<template>
    <div class="form">
        <div class="field">
            <label class="label" for="buyin-amount">Amount</label>
            <input class="numberfield" v-model:number="amount" v-on:focus="focus" ref="buyin" id="buyin-amount" type="text" pattern="[0-9]*">
        </div>
        <div class="field" v-if="$_isInGame">
            <label class="label" for="buyin-stack">Stack Size</label>
            <input class="numberfield" v-model:number="stack" v-on:focus="focus" id="buyin-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <custom-button v-on:click="buyin" type="action" text="Buy In" />
            <custom-button v-on:click="cancel" text="Cancel" />
        </div>
    </div>
</template>

<script>
    import validate from '@/validate';
    import forms from '@/forms';
    import { CustomButton } from '@/components/Common';
    import { BunchMixin, CashgameMixin, PlayerMixin } from '@/mixins';

    export default {
        props: {
            isActive: {
                type: Boolean
            }
        },
        components: {
            CustomButton
        },
        mixins: [
            BunchMixin,
            CashgameMixin,
            PlayerMixin
        ],
        computed: {
            currentPlayer() {
                this.$_getPlayer(this.$_playerId);
            },
            playerName() {
                if (!this.currentPlayer)
                    return '';
                return this.currentPlayer.name;
            },
            playerColor() {
                if (!this.currentPlayer)
                    return '';
                return this.currentPlayer.color;
            },
            hasErrors() {
                return this.buyinError === null && this.stackError === null;
            }
        },
        mounted: function () {
            this.amount = this.$_defaultBuyin;
        },
        watch: {
            isActive: function (val) {
                if (val) {
                    this.$refs.buyin.focus();
                }
            },
            $_defaultBuyin: function (val) {
                this.amount = val;
            }
        },
        methods: {
            buyin() {
                this.validateForm();
                if (!this.hasErrors) {
                    if (this.$_isInGame) {
                        this.$_buyin(this.amount, this.stack);
                    } else {
                        this.$_firstBuyin(this.amount, this.stack, this.playerName, this.playerColor);
                    }
                }
            },
            cancel() {
                this.$_hideForms();
            },
            focus(event) {
                forms.selectAll(event.target);
            },
            validateForm() {
                this.clearErrors();
                if (validate.intRange(this.amount, 1))
                    this.buyinError = 'Buyin must be greater than zero';
                if (validate.intRange(this.stack, 0))
                    this.stackError = 'Stack can\'t be negative';
            },
            clearErrors() {
                this.buyinError = null;
                this.stackError = null;
            }
        },
        data: function () {
            return {
                amount: 0,
                stack: 0,
                buyinError: null,
                stackError: null
            }
        }
    };
</script>

<style>

</style>
