<template>
  <Layout :ready="ready">
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
            {{ bunch.description }}
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
import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
import { computed, onMounted, ref } from 'vue';
import useBunches from '@/composables/useBunches';
import useUsers from '@/composables/useUsers';
import useFormatter from '@/composables/useFormatter';
import { useBunchQuery, useUpdateBunchMutation, bunchQueryKey } from '@/composables/bunchQueries';
import { useRoute } from 'vue-router';
import roles from '@/roles';
import { useQueryClient } from 'vue-query';

const users = useUsers();
const bunches = useBunches();
const formatter = useFormatter();
const route = useRoute();
const bunchQuery = useBunchQuery(route.params.slug as string);
const queryClient = useQueryClient();

const onUpdateSuccess = () => {
  queryClient.invalidateQueries(bunchQueryKey(route.params.slug as string));
};

const { mutate: updateBunch } = useUpdateBunchMutation(route.params.slug as string, onUpdateSuccess);

const isEditing = ref(false);
const errorMessage = ref<string | null>(null);
const formDescription = ref<string>();
const formHouseRules = ref<string>();
const formDefaultBuyin = ref<number | null>(null);
const formTimezone = ref<string>();
const formCurrencySymbol = ref<string>();
const formCurrencyLayout = ref<string>();

const bunch = computed(() => {
  return bunchQuery.data.value!;
});

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
  return formatter.formatCurrency(123);
});

const currencySymbol = computed(() => {
  return bunch.value.currencySymbol;
});

const currencyLayout = computed(() => {
  return bunch.value.currencyLayout;
});

const isManager = computed((): boolean => {
  return bunch.value.role === roles.manager || bunch.value.role === roles.admin;
});

const showEditForm = () => {
  formDescription.value = description.value;
  formHouseRules.value = houseRules.value;
  formDefaultBuyin.value = defaultBuyin.value;
  formTimezone.value = timezone.value;
  formCurrencySymbol.value = currencySymbol.value;
  formCurrencyLayout.value = currencyLayout.value;
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

  updateBunch(postData);
  hideEditForm();
};

const ready = computed(() => {
  return bunchQuery.isSuccess.value;
});

const init = () => {
  users.requireUser();
  bunches.loadBunch();
};

onMounted(() => {
  init();
});
</script>
