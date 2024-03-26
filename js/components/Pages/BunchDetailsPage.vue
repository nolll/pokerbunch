<template>
  <Layout :require-user="true" :ready="ready">
    <template v-slot:top-nav>
      <BunchNavigation />
    </template>

    <template v-slot:default>
      <PageSection>
        <Block>
          <PageHeading :text="bunchName" />
        </Block>

        <template v-if="isEditing">
          <Block v-if="isManager">
            <p>
              <label class="label" for="description">Description</label>
              <input class="textfield" v-model="formDescription" id="description" type="text" />
            </p>
            <p>
              <label class="label" for="houseRules">House Rules</label>
              <textarea class="textfield" v-model="formHouseRules" id="houseRules"></textarea>
            </p>
            <p>
              <label class="label" for="defaultBuyin">Default buyin</label>
              <input class="textfield" v-model="formDefaultBuyin" id="defaultBuyin" type="text" />
            </p>
            <p>
              <label class="label" for="timezone">Timezone</label>
              <TimezoneDropdown v-model="formTimezone" />
            </p>
            <p>
              <label class="label" for="currencySymbol">Currency Symbol</label>
              <input class="textfield" v-model="formCurrencySymbol" id="currencySymbol" type="text" />
            </p>
            <p>
              <label class="label" for="currencyLayout">Currency Layout</label>
              <CurrencyLayoutDropdown v-model="formCurrencyLayout" :symbol="formCurrencySymbol" />
            </p>
            <ErrorMessage :message="errorMessage"></ErrorMessage>
            <div class="buttons">
              <CustomButton @click="save" text="Save" type="action" />
              <CustomButton @click="cancel" text="Cancel" />
            </div>
          </Block>
        </template>

        <template v-else>
          <Block v-if="hasDescription">
            {{ description }}
          </Block>

          <Block v-if="hasHouseRules"><h2>House Rules</h2></Block>
          <Block v-if="hasHouseRules">
            <p>
              {{ houseRules }}
            </p>
          </Block>

          <Block><h2>Settings</h2></Block>
          <Block>
            <ValueList>
              <ValueListKey>Default Buyin</ValueListKey>
              <ValueListValue>{{ defaultBuyin }}</ValueListValue>
              <ValueListKey>Timezone</ValueListKey>
              <ValueListValue>{{ timezone }}</ValueListValue>
              <ValueListKey>Currency Format</ValueListKey>
              <ValueListValue>{{ currencyFormat }}</ValueListValue>
            </ValueList>
          </Block>

          <Block v-if="isManager">
            <CustomButton @click="showEditForm" text="Edit Bunch" type="action" />
          </Block>
        </template>
      </PageSection>
    </template>
  </Layout>
</template>

<script setup lang="ts">
import Layout from '@/components/Layouts/Layout.vue';
import BunchNavigation from '@/components/Navigation/BunchNavigation.vue';
import Block from '@/components/Common/Block.vue';
import PageHeading from '@/components/Common/PageHeading.vue';
import PageSection from '@/components/Common/PageSection.vue';
import CustomButton from '@/components/Common/CustomButton.vue';
import ErrorMessage from '@/components/Common/ErrorMessage.vue';
import CurrencyLayoutDropdown from '@/components/CurrencyLayoutDropdown.vue';
import TimezoneDropdown from '@/components/TimezoneDropdown.vue';
import ValueList from '@/components/Common/ValueList/ValueList.vue';
import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
import api from '@/api';
import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
import { computed, ref } from 'vue';
import useBunch from '@/composables/useBunch';
import useParams from '@/composables/useParams';
import format from '@/format';
import { useMutation, useQueryClient } from '@tanstack/vue-query';
import { bunchKey, bunchListKey, userBunchListKey } from '@/queries/queryKeys';

const { slug } = useParams();
const { bunch, isManager, localization, bunchReady } = useBunch(slug.value);
const queryClient = useQueryClient();

const isEditing = ref(false);
const errorMessage = ref<string | null>(null);
const formDescription = ref<string>();
const formHouseRules = ref<string>();
const formDefaultBuyin = ref<number | null>(null);
const formTimezone = ref<string>();
const formCurrencySymbol = ref<string>();
const formCurrencyLayout = ref<string>();

const bunchName = computed(() => {
  return bunch.value.name;
});

const description = computed(() => {
  return bunch.value.description;
});

const hasDescription = computed(() => {
  return !!description.value;
});

const houseRules = computed(() => {
  return bunch.value.houseRules;
});

const hasHouseRules = computed(() => {
  return !!houseRules.value;
});

const defaultBuyin = computed(() => {
  return bunch.value.defaultBuyin;
});

const timezone = computed(() => {
  return bunch.value.timezone;
});

const currencyFormat = computed(() => {
  return format.currency(123, localization.value);
});

const showEditForm = () => {
  formDescription.value = bunch.value.description;
  formHouseRules.value = bunch.value.houseRules;
  formDefaultBuyin.value = bunch.value.defaultBuyin;
  formTimezone.value = bunch.value.timezone;
  formCurrencySymbol.value = bunch.value.currencySymbol;
  formCurrencyLayout.value = bunch.value.currencyLayout;
  isEditing.value = true;
};

const hideEditForm = () => {
  isEditing.value = false;
};

const cancel = () => {
  hideEditForm();
};

const saveMutation = useMutation({
  mutationFn: async () => {
    const postData: ApiParamsUpdateBunch = {
      description: formDescription.value,
      houseRules: formHouseRules.value,
      defaultBuyin: formDefaultBuyin.value,
      timezone: formTimezone.value,
      currencySymbol: formCurrencySymbol.value,
      currencyLayout: formCurrencyLayout.value,
    };

    await api.updateBunch(bunch.value.id, postData);
  },
  onSuccess: () => {
    queryClient.invalidateQueries({ queryKey: userBunchListKey(true) });
    queryClient.invalidateQueries({ queryKey: bunchListKey() });
    queryClient.invalidateQueries({ queryKey: bunchKey(slug.value) });
    hideEditForm();
  },
  onError: () => {
    errorMessage.value = 'Server error';
  },
});

const save = () => {
  errorMessage.value = null;

  if (!formDefaultBuyin.value) {
    errorMessage.value = 'Please enter a default buyin';
    return;
  }

  if (!formTimezone.value) {
    errorMessage.value = 'Please select a timezone';
    return;
  }

  if (!formCurrencySymbol.value) {
    errorMessage.value = 'Please enter a currency symbol';
    return;
  }

  if (!formCurrencyLayout.value) {
    errorMessage.value = 'Please select a currency layout';
    return;
  }

  saveMutation.mutate();
};

const ready = computed(() => {
  return bunchReady.value;
});
</script>
