<template>
  <div class="form">
    <div class="field">
      <label class="label" for="buyin-amount">Amount</label>
      <input
        class="numberfield"
        v-model="strAmount"
        v-on:focus="focus"
        id="buyin-amount"
        type="text"
        inputmode="numeric"
        pattern="[0-9]*"
      />
    </div>
    <div class="field" v-if="isPlayerInGame">
      <label class="label" for="buyin-stack">Stack Size</label>
      <input
        class="numberfield"
        v-model="strStack"
        v-on:focus="focus"
        id="buyin-stack"
        type="text"
        inputmode="numeric"
        pattern="[0-9]*"
      />
    </div>
    <div class="buttons">
      <CustomButton v-on:click="buyin" type="action" text="Buy In" />
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
  isPlayerInGame: boolean;
}>();

const emit = defineEmits(['buyin', 'cancel']);

const strAmount = ref('0');
const strStack = ref('0');
const buyinError = ref<string | null>(null);
const stackError = ref<string | null>(null);

const hasErrors = computed(() => {
  return buyinError.value === null && stackError.value === null;
});

const amount = computed(() => {
  return forms.parseInt(strAmount.value);
});

const stack = computed(() => {
  return forms.parseInt(strStack.value);
});

const buyin = () => {
  validateForm();
  if (!hasErrors.value) {
    emit('buyin', amount.value, stack.value);
  }
};

const cancel = () => {
  emit('cancel');
};

const focus = (e: FocusEvent) => {
  if (e.target) forms.selectAll(e.target as HTMLInputElement);
};

const validateForm = () => {
  clearErrors();
  if (validate.intRange(amount.value, 1)) buyinError.value = 'Buyin must be greater than zero';
  if (validate.intRange(stack.value, 0)) stackError.value = "Stack can't be negative";
};

const clearErrors = () => {
  buyinError.value = null;
  stackError.value = null;
};

onMounted(() => {
  strAmount.value = props.defaultBuyin.toString();
});
</script>
