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

<script lang="ts">
    import { Component, Prop, Mixins } from 'vue-property-decorator';
    import { BunchMixin, CashgameMixin, FormatMixin } from '@/mixins';
    import dayjs from 'dayjs';
    import { DetailedCashgameResponseAction } from '@/response/DetailedCashgameResponseAction';
    import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';
import format from '@/format';

    @Component
    export default class PlayerAction extends Mixins(
        BunchMixin,
        CashgameMixin,
        FormatMixin
    ) {
        @Prop() readonly action!: DetailedCashgameResponseAction;

        isFormVisible = false;
        changedTime: string | null = null;
        changedStack: number | null = null;
        changedAdded: number | null = null;

        get formTime() {
            if (this.changedTime !== null)
                return this.changedTime;
            return this.action.time;
        }

        get formStack() {
            if (this.changedStack !== null)
                return this.changedStack;
            return this.action.stack;
        }

        get formAdded() {
            if (this.changedAdded !== null)
                return this.changedAdded;
            return this.action.added;
        }

        get formattedTime() {
            return format.hourMinute(this.action.time);
        }

        get formattedAmount() {
            return this.$_formatCurrency(this.amount);
        }

        get amount() {
            if (this.action.type === DetailedCashgameResponseActionType.Buyin && this.action.added)
                return this.action.added;
            return this.action.stack;
        }

        get typeName(): string {
            if (this.action.type === DetailedCashgameResponseActionType.Buyin)
                return 'Buyin';
            if (this.action.type === DetailedCashgameResponseActionType.Cashout)
                return 'Cashout';
            return 'Report';
        }

        get canEdit() {
            return this.$_isManager;
        }

        get showAddedField() {
            return this.action.type === DetailedCashgameResponseActionType.Buyin;
        }

        clickEdit() {
            this.showForm();
        }

        clickDelete() {
            if (this.action.id && window.confirm('Do you want to delete this action?')) {
                this.$_deleteAction(this.action.id);
            }
        }

        clickCancel() {
            this.hideForm();
        }

        clickSave() {
            const data = {
                id: this.action.id,
                time: this.formTime,
                stack: this.formStack,
                added: this.formAdded
            };
            this.$_saveAction(data);
            this.hideForm();
            this.changedTime = null;
            this.changedStack = null;
            this.changedAdded = null;
        }

        showForm() {
            this.isFormVisible = true;
        }

        hideForm() {
            this.isFormVisible = false;
        }

        updateTime(e: Event) {
            const el = e.target as HTMLInputElement
            this.changedTime = el.value;
        }

        updateStack(e: Event) {
            const el = e.target as HTMLInputElement
            this.changedStack = parseInt(el.value);
        }

        updateAdded(e: Event) {
            const el = e.target as HTMLInputElement
            this.changedAdded = parseInt(el.value);
        }
    }
</script>
