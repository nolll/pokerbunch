<template>
  <div class="form">
    <div class="field">
      <label class="label" for="report-stack">Stack Size</label>
      <input
        class="numberfield"
        v-model="strStack"
        v-on:focus="focus"
        id="report-stack"
        type="text"
        inputmode="numeric"
        pattern="[0-9]*"
      />
    </div>
    <div class="buttons">
      <CustomButton v-on:click="report" type="action" text="Report" />
      <CustomButton v-on:click="cancel" text="Cancel" />
    </div>
  </div>
</template>

<script setup lang="ts">
import validate from '@/validate';
import forms from '@/forms';
import { CustomButton } from '@/components/Common';
import { computed, onMounted, ref } from 'vue';

const props = defineProps<{
  defaultBuyin: number;
}>();

const emit = defineEmits(['report', 'cancel']);

const strStack = ref('0');
const stackError = ref<string | null>(null);

const hasErrors = computed(() => {
  return stackError.value === null;
});

const stack = computed(() => {
  return forms.parseInt(strStack.value);
});

const report = () => {
  validateForm();
  if (!hasErrors.value) emit('report', stack.value);
};

const cancel = () => {
  emit('cancel');
};

const focus = (e: Event) => {
  var el = e.target as HTMLInputElement;
  forms.selectAll(el);
};

const validateForm = () => {
  clearErrors();
  if (validate.intRange(stack.value, 0)) stackError.value = "Stack can't be negative";
};

const clearErrors = () => {
  stackError.value = null;
};

onMounted(() => {
  strStack.value = props.defaultBuyin.toString();
});
</script>
