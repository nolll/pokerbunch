import html from './buyin-form.html';
import validate from '../../validate';
import forms from '../../forms';

export default {
    template: html,
    props: ['stack', 'amount', 'isActive', 'isInGame'],
    computed: {
        hasErrors: function() {
            return this.buyinError === null && this.stackError === null;
        }
    },
    watch: {
        'isActive': function(val) {
            if (val) {
                this.$refs.buyin.focus();
            }
        }
    },
    methods: {
        buyin: function() {
            this.validateForm();
            if (!this.hasErrors)
                this.eventHub.$emit('buyin', this.amount, this.stack);
        },
        cancel: function() {
            this.eventHub.$emit('hide-forms');
        },
        focus: function(event) {
            forms.selectAll(event.target);
        },
        validateForm: function() {
            this.clearErrors();
            if (validate.intRange(this.amount, 1))
                this.buyinError = 'Buyin must be greater than zero';
            if (validate.intRange(this.stack, 0))
                this.stackError = 'Stack can\'t be negative';
        },
        clearErrors: function() {
            this.buyinError = null;
            this.stackError = null;
        }
    },
    data: function() {
        return {
            buyinError: null,
            stackError: null
        }
    }
};