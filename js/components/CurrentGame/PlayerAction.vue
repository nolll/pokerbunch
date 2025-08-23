<template>
  <div>
    <div v-if="isFormVisible">
      <div>type: {{ action.type }}</div>
      <div>time: <input type="text" v-model="strChangedTime" /></div>
      <div>stack: <input type="text" class="numberfield" v-model="strChangedStack" /></div>
      <div v-if="showAddedField">added: <input type="text" class="numberfield" v-model="strChangedAdded" /></div>
      <div>
        <button class="button" @click="clickCancel">Cancel</button>
        <button class="button button--action" @click="clickSave">Save</button>
      </div>
    </div>
    <div v-else>
      {{ formattedTime }} {{ typeName }}: {{ formattedAmount }}
      <button class="button button--action" @click="clickEdit" v-if="canEdit">Edit</button>
      <button class="button button--action" @click="clickDelete" v-if="canEdit">Delete</button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';
import format from '@/format';
import { computed, ref } from 'vue';
import { Localization } from '@/models/Localization';
import { DetailedCashgameAction } from '@/models/DetailedCashgameAction';
import { SaveActionEmitData } from '@/models/SaveActionEmitData';
import forms from '@/forms';
import dayjs from 'dayjs';

const props = defineProps<{
  action: DetailedCashgameAction;
  localization: Localization;
  canEdit: boolean;
}>();

const emit = defineEmits<{
  saveAction: [data: SaveActionEmitData];
  deleteAction: [data: string];
}>();

const isFormVisible = ref(false);

const strChangedTime = ref('');
const strChangedStack = ref('');
const strChangedAdded = ref('');

const formattedTime = computed(() => format.hourMinute(props.action.time));
const formattedAmount = computed(() => format.currency(amount.value, props.localization));

const amount = computed(() => {
  if (props.action.type === DetailedCashgameResponseActionType.Buyin && props.action.added) return props.action.added;
  return props.action.stack;
});

const typeName = computed((): string => {
  if (props.action.type === DetailedCashgameResponseActionType.Buyin) return 'Buyin';
  if (props.action.type === DetailedCashgameResponseActionType.Cashout) return 'Cashout';
  return 'Report';
});

const showAddedField = computed(() => {
  return props.action.type === DetailedCashgameResponseActionType.Buyin;
});

const clickEdit = () => {
  showForm();
};

const clickDelete = () => {
  if (props.action.id && window.confirm('Do you want to delete this action?')) {
    emit('deleteAction', props.action.id);
  }
};

const clickCancel = () => {
  hideForm();
};

const clickSave = () => {
  const data: SaveActionEmitData = {
    id: props.action.id,
    time: dayjs(strChangedTime.value).utc().toDate(),
    stack: forms.parseInt(strChangedStack.value),
    added: forms.parseInt(strChangedAdded.value),
  };
  emit('saveAction', data);
  hideForm();
};

const showForm = () => {
  setFormValues();
  isFormVisible.value = true;
};

const hideForm = () => {
  isFormVisible.value = false;
};

const setFormValues = () => {
  strChangedTime.value = format.localTime(props.action.time);
  strChangedStack.value = props.action.stack?.toString() ?? '';
  strChangedAdded.value = props.action.added?.toString() ?? '';
};
</script>
