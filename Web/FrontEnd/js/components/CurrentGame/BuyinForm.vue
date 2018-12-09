<template>
    <div class="form">
        <div class="field">
            <label class="label" for="buyin-amount">Amount</label>
            <input class="numberfield" v-model:number="amount" v-on:focus="focus" ref="buyin" id="buyin-amount" type="text" pattern="[0-9]*">
        </div>
        <div class="field" v-if="isInGame">
            <label class="label" for="buyin-stack">Stack Size</label>
            <input class="numberfield" v-model:number="stack" v-on:focus="focus" id="buyin-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <button v-on:click="buyin" class="button button--action">Buy In</button>
            <button v-on:click="cancel" class="button">Cancel</button>
        </div>
    </div>
</template>

<script>
    import { mapState, mapGetters } from 'vuex';
    import validate from '../../validate';
    import forms from '../../forms';
    import { CURRENT_GAME } from '@/store-names';

    export default {
        props: ['isActive'],
        computed: {
            ...mapState(CURRENT_GAME, {
                defaultBuyin: state => state.defaultBuyin
            }),
            ...mapGetters(CURRENT_GAME, {
                isInGame: getters => getters.isInGame
            }),
            hasErrors() {
                return this.buyinError === null && this.stackError === null;
            }
        },
        mounted: function () {
            this.amount = this.defaultBuyin;
        },
        watch: {
            isActive: function (val) {
                if (val) {
                    this.$refs.buyin.focus();
                }
            },
            defaultBuyin: function (val) {
                this.amount = val;
            }
        },
        methods: {
            buyin() {
                this.validateForm();
                if (!this.hasErrors)
                    this.$store.dispatch('currentGame/buyin', { amount: this.amount, stack: this.stack });
            },
            cancel() {
                this.$store.dispatch('currentGame/hideForms');
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
