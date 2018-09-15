<template>
    <div class="form">
        <div class="field">
            <label class="label" for="buyin-amount">Amount</label>
            <input class="numberfield" v-model.number="amount" v-on:focus="focus" ref="buyin" id="buyin-amount" type="text" pattern="[0-9]*">
        </div>
        <div class="field" v-if="isInGame">
            <label class="label" for="buyin-stack">Stack Size</label>
            <input class="numberfield" v-model.number="stack" v-on:focus="focus" id="buyin-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <button v-on:click="buyin" class="button button--action">Buy In</button>
            <button v-on:click="cancel" class="button">Cancel</button>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import validate from '../../validate';
    import forms from '../../forms';

    export default {
        props: ['isActive'],
        computed: {
            ...mapGetters('currentGame', ['isInGame']),
            hasErrors: function () {
                return this.buyinError === null && this.stackError === null;
            }
        },
        watch: {
            'isActive': function (val) {
                if (val) {
                    this.$refs.buyin.focus();
                }
            }
        },
        methods: {
            buyin: function () {
                this.validateForm();
                if (!this.hasErrors)
                    this.$store.dispatch('currentGame/buyin', { amount: this.amount, stack: this.stack });
            },
            cancel: function () {
                this.$store.dispatch('currentGame/hideForms');
            },
            focus: function (event) {
                forms.selectAll(event.target);
            },
            validateForm: function () {
                this.clearErrors();
                if (validate.intRange(this.amount, 1))
                    this.buyinError = 'Buyin must be greater than zero';
                if (validate.intRange(this.stack, 0))
                    this.stackError = 'Stack can\'t be negative';
            },
            clearErrors: function () {
                this.buyinError = null;
                this.stackError = null;
            }
        },
        data: function () {
            return {
                stack: 0,
                amount: 0,
                buyinError: null,
                stackError: null
            }
        }
    };
</script>

<style>

</style>
