<template>
    <div class="form">
        <div class="field">
            <label class="label" for="cashout-stack">Stack Size</label>
            <input class="numberfield" v-model.number="stack" v-on:focus="focus" ref="stack" id="cashout-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <button v-on:click="cashout" class="button button--action">Cash Out</button>
            <button v-on:click="cancel" class="button">Cancel</button>
        </div>
    </div>
</template>

<script>
    import { mapState } from 'vuex';
    import validate from '../../validate';
    import forms from '../../forms';
    import { CURRENT_GAME } from '../../store-names';

    export default {
        props: ['isActive'],
        computed: {
            ...mapState(CURRENT_GAME, {
                defaultBuyin: state => state.defaultBuyin
            }),
            hasErrors: function () {
                return this.stackError === null;
            }
        },
        mounted: function () {
            this.stack = this.defaultBuyin;
        },
        watch: {
            isActive: function (val) {
                if (val) {
                    this.$refs.stack.focus();
                }
            },
            defaultBuyin: function (val) {
                this.stack = val;
            }
        },
        methods: {
            cashout() {
                this.validateForm();
                if (!this.hasErrors)
                    this.$store.dispatch('currentGame/cashout', { stack: this.stack });
            },
            cancel() {
                this.$store.dispatch('currentGame/hideForms');
            },
            focus(event) {
                forms.selectAll(event.target);
            },
            validateForm() {
                this.clearErrors();
                if (validate.intRange(this.stack, 0))
                    this.stackError = 'Stack can\'t be negative';
            },
            clearErrors() {
                this.stackError = null;
            }
        },
        data: function () {
            return {
                stack: 0,
                stackError: null
            }
        }
    };
</script>

<style>

</style>
