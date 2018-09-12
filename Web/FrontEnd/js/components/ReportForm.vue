<template>
    <div class="form">
        <div class="field">
            <label class="label" for="report-stack">Stack Size</label>
            <input class="numberfield" v-model.number="stack" v-on:focus="focus" ref="stack" id="report-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <button v-on:click="report" class="button button--action">Report</button>
            <button v-on:click="cancel" class="button">Cancel</button>
        </div>
    </div>
</template>

<script>
    import validate from '../validate';
    import forms from '../forms';

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
            },
        },
        methods: {
            report: function () {
                this.validateForm();
                if (!this.hasErrors)
                    this.$store.dispatch('currentGame/report', { stack: this.stack });
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

<style scoped>

</style>
