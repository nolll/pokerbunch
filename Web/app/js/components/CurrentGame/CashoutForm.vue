<template>
  <div class="form">
    <div class="field">
      <label class="label" for="cashout-stack">Stack Size</label>
      <input class="numberfield" v-model="stack" v-on:focus="focus" ref="stack" id="cashout-stack" type="text" pattern="[0-9]*" />
    </div>
    <div class="buttons">
      <CustomButton v-on:click="cashout" type="action" text="Cash Out" />
      <CustomButton v-on:click="cancel" text="Cancel" />
    </div>
  </div>
</template>

<script setup lang="ts">
import validate from '@/validate';
import forms from '@/forms';
import CustomButton from '@/components/Common/CustomButton.vue';
import { computed, onMounted, ref } from 'vue';

const props = defineProps<{
  defaultBuyin: number;
}>();

const emit = defineEmits(['cashout', 'cancel']);

const stack = ref(0);
const stackError = ref<string | null>(null);

const hasErrors = computed(() => {
  return stackError.value === null;
});

const cashout = () => {
  validateForm();
  if (!hasErrors.value) emit('cashout', stack.value);
};

const cancel = () => {
  emit('cancel');
};

const focus = (e: FocusEvent) => {
  if (e.target) forms.selectAll(e.target as HTMLInputElement);
};

const validateForm = () => {
  clearErrors();
  if (validate.intRange(stack.value, 0)) stackError.value = "Stack can't be negative";
};

const clearErrors = () => {
  stackError.value = null;
};

onMounted(() => {
  stack.value = props.defaultBuyin;
});
</script>
