<template>
    <div class="form">
        <div class="field">
            <label class="label" for="report-stack">Stack Size</label>
            <input class="numberfield" v-model.number="stack" v-on:focus="focus" ref="stack" id="report-stack" type="text" pattern="[0-9]*">
        </div>
        <div class="buttons">
            <CustomButton v-on:click="report" type="action" text="Report" />
            <CustomButton v-on:click="cancel" text="Cancel" />
        </div>
    </div>
</template>

<script lang="ts">
    import { Component, Prop, Vue, Watch } from 'vue-property-decorator';
    import validate from '@/validate';
    import forms from '@/forms';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import { BunchMixin } from '@/mixins';

    @Component({
        components: {
            CustomButton
        }
    })
    export default class ReportForm extends Vue {
        @Prop() readonly defaultBuyin!: number;

        stack = 0;
        stackError: string | null = null;

        get hasErrors() {
            return this.stackError === null;
        }

        report() {
            this.validateForm();
            if (!this.hasErrors)
                this.$emit('report', this.stack);
        }

        cancel() {
            this.$emit('cancel');
        }

        focus(e: Event) {
            var el = e.target as HTMLInputElement;
            forms.selectAll(el);
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
            this.stack = this.defaultBuyin;
        }
    }
</script>
