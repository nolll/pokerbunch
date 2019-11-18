<template>
    <div>
        <div v-if="isFormVisible">
            <div>
                type: {{action.type}}
            </div>
            <div>
                time: <input type="text" :value="formTime" @input="updateTime" />
            </div>
            <div>
                stack: <input type="text" :value="formStack" @input="updateStack" />
            </div>
            <div v-if="showAddedField">
                added: <input type="text" :value="formAdded" @input="updateAdded" />
            </div>
            <div>
                <button @click="clickCancel">Cancel</button>
                <button @click="clickSave">Save</button>
            </div>
        </div>
        <div v-else>
            {{formattedTime}} {{typeName}}: {{formattedAmount}}
            <button @click="clickEdit" v-if="canEdit">Edit</button>
            <button @click="clickDelete" v-if="canEdit">Delete</button>
        </div>
    </div>
</template>

<script>
    import { mapGetters } from 'vuex';
    import { FormatMixin } from '@/mixins';
    import actionTypes from '@/action-types';
    import moment from 'moment';
    import { BUNCH } from '@/store-names';

    export default {
        mixins: [
            FormatMixin
        ],
        props: {
            action: {
                type: Object
            }
        },
        computed: {
            ...mapGetters(BUNCH, [
                'isManager'
            ]),
            formTime() {
                if (this.changedTime !== null)
                    return this.changedTime;
                return this.action.time;
            },
            formStack() {
                if (this.changedStack !== null)
                    return this.changedStack;
                return this.action.stack;
            },
            formAdded() {
                if (this.changedAdded !== null)
                    return this.changedAdded;
                return this.action.added;
            },
            formattedTime() {
                return moment(this.action.time).format('HH:mm');
            },
            formattedAmount() {
                return this.formatCurrency(this.amount);
            },
            amount() {
                if (this.action.type === actionTypes.buyin)
                    return this.action.added;
                return this.action.stack;
            },
            typeName() {
                if (this.action.type === actionTypes.buyin)
                    return 'Buyin';
                if (this.action.type === actionTypes.cashout)
                    return 'Cashout';
                return 'Report';
            },
            canEdit() {
                return this.isManager;
            },
            showAddedField() {
                return this.action.type === actionTypes.buyin;
            }
        },
        methods: {
            clickEdit() {
                this.showForm();
            },
            clickDelete() {
                if (window.confirm('Do you want to delete this action?')) {
                    this.$store.dispatch('cashgame/deleteAction', { id: this.action.id });
                }
            },
            clickCancel() {
                this.hideForm();
            },
            clickSave() {
                const data = {
                    id: this.action.id,
                    time: this.formTime,
                    stack: this.formStack,
                    added: this.formAdded
                };
                this.$store.dispatch('cashgame/saveAction', data);
                this.hideForm();
                this.changedTime = null;
                this.changedStack = null;
                this.changedAdded = null;
            },
            showForm() {
                this.isFormVisible = true;
            },
            hideForm() {
                this.isFormVisible = false;
            },
            updateTime(e) {
                this.changedTime = e.target.value;
            },
            updateStack(e) {
                this.changedStack = parseInt(e.target.value);
            },
            updateAdded(e) {
                this.changedAdded = parseInt(e.target.value);
            }
        },
        data: function () {
            return {
                isFormVisible: false,
                changedTime: null,
                changedStack: null,
                changedAdded: null
            };
        }
    };
</script>

<style>
</style>
