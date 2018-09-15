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
    import validate from '../../validate';
    import forms from '../../forms';

    export default {
        props: ['isActive'],
        computed: {
            hasErrors: function () {
                return this.stackError === null;
            }
        },
        watch: {
            'isActive': function (val) {
                if (val) {
                    this.$refs.stack.focus();
                }
            }
        },
        methods: {
            cashout: function () {
                this.validateForm();
                if (!this.hasErrors)
                    this.$store.dispatch('currentGame/cashout', { stack: this.stack });
            },
            cancel: function () {
                this.$store.dispatch('currentGame/hideForms');
            },
            focus: function (event) {
                forms.selectAll(event.target);
            },
            validateForm: function () {
                this.clearErrors();
                if (validate.intRange(this.stack, 0))
                    this.stackError = 'Stack can\'t be negative';
            },
            clearErrors: function () {
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
