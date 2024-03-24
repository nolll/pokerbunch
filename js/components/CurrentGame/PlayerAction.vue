<template>
  <div>
    <div v-if="isFormVisible">
      <div>type: {{ action.type }}</div>
      <div>time: <input type="text" :value="formTime" @input="updateTime" /></div>
      <div>stack: <input type="text" :value="formStack" @input="updateStack" /></div>
      <div v-if="showAddedField">added: <input type="text" :value="formAdded" @input="updateAdded" /></div>
      <div>
        <button @click="clickCancel">Cancel</button>
        <button @click="clickSave">Save</button>
      </div>
    </div>
    <div v-else>
      {{ formattedTime }} {{ typeName }}: {{ formattedAmount }}
      <button @click="clickEdit" v-if="canEdit">Edit</button>
      <button @click="clickDelete" v-if="canEdit">Delete</button>
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
const changedTime = ref<string | null>(null);
const changedStack = ref<number | null>(null);
const changedAdded = ref<number | null>(null);

const formTime = computed(() => {
  if (changedTime.value !== null) return changedTime.value;
  return format.isoTime(props.action.time);
});

const formStack = computed(() => {
  if (changedStack.value !== null) return changedStack.value;
  return props.action.stack;
});

const formAdded = computed(() => {
  if (changedAdded.value !== null) return changedAdded.value;
  return props.action.added;
});

const formattedTime = computed(() => {
  return format.hourMinute(props.action.time);
});

const formattedAmount = computed(() => {
  return format.currency(amount.value, props.localization);
});

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
    time: formTime.value,
    stack: formStack.value,
    added: formAdded.value,
  };
  emit('saveAction', data);
  hideForm();
  changedTime.value = null;
  changedStack.value = null;
  changedAdded.value = null;
};

const showForm = () => {
  isFormVisible.value = true;
};

const hideForm = () => {
  isFormVisible.value = false;
};

const updateTime = (e: Event) => {
  const el = e.target as HTMLInputElement;
  changedTime.value = el.value;
};

const updateStack = (e: Event) => {
  const el = e.target as HTMLInputElement;
  changedStack.value = parseInt(el.value);
};

const updateAdded = (e: Event) => {
  const el = e.target as HTMLInputElement;
  changedAdded.value = parseInt(el.value);
};
</script>
