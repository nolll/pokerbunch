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
import CurrencyLayoutDropdown from '@/components/CurrencyLayoutDropdown.vue';
import TimezoneDropdown from '@/components/TimezoneDropdown.vue';
import ValueList from '@/components/Common/ValueList/ValueList.vue';
import ValueListKey from '@/components/Common/ValueList/ValueListKey.vue';
import ValueListValue from '@/components/Common/ValueList/ValueListValue.vue';
import api from '@/api';
import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
import { computed, onMounted, ref } from 'vue';
import useBunches from '@/composables/useBunches';
import useFormatter from '@/composables/useFormatter';
import useBunch from '@/composables/useBunch';
import useParams from '@/composables/useParams';
import format from '@/format';

const { slug } = useParams();
const { localization, bunchReady } = useBunch(slug.value);
const bunches = useBunches();
const formatter = useFormatter();

const isEditing = ref(false);
const errorMessage = ref<string | null>(null);
const formDescription = ref<string>();
const formHouseRules = ref<string>();
const formDefaultBuyin = ref<number | null>(null);
const formTimezone = ref<string>();
const formCurrencySymbol = ref<string>();
const formCurrencyLayout = ref<string>();

const bunchName = computed(() => {
  return bunches.bunchName.value;
});

const description = computed(() => {
  return bunches.description.value;
});

const hasDescription = computed(() => {
  return !!description.value;
});

const houseRules = computed(() => {
  return bunches.houseRules.value;
});

const hasHouseRules = computed(() => {
  return !!houseRules.value;
});

const defaultBuyin = computed(() => {
  return bunches.bunch.value?.defaultBuyin;
});

const timezone = computed(() => {
  return bunches.bunch.value?.timezone;
});

const currencyFormat = computed(() => {
  return format.currency(123, localization.value);
});

const currencySymbol = computed(() => {
  return bunches.bunch.value?.currencySymbol;
});

const currencyLayout = computed(() => {
  return bunches.bunch.value?.currencyLayout;
});

const isManager = computed(() => {
  return bunches.isManager.value;
});

const showEditForm = () => {
  formDescription.value = bunches.bunch.value.description;
  formHouseRules.value = bunches.bunch.value.houseRules;
  formDefaultBuyin.value = bunches.bunch.value.defaultBuyin;
  formTimezone.value = bunches.bunch.value.timezone;
  formCurrencySymbol.value = bunches.bunch.value.currencySymbol;
  formCurrencyLayout.value = bunches.bunch.value.currencyLayout;
  isEditing.value = true;
};

const hideEditForm = () => {
  isEditing.value = false;
};

const cancel = () => {
  hideEditForm();
};

const save = async () => {
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

  const postData: ApiParamsUpdateBunch = {
    description: formDescription.value,
    houseRules: formHouseRules.value,
    defaultBuyin: formDefaultBuyin.value,
    timezone: formTimezone.value,
    currencySymbol: formCurrencySymbol.value,
    currencyLayout: formCurrencyLayout.value,
  };

  await api.updateBunch(bunches.bunch.value.id, postData);
  bunches.refreshBunch();
  hideEditForm();
};

const ready = computed(() => {
  return bunchReady.value && bunches.bunchReady.value;
});

const init = () => {
  bunches.loadBunch();
};

onMounted(() => {
  init();
});
</script>
