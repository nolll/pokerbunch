<template>
    <Layout :ready="ready">
        <PageSection>
            <Block>
                <PageHeading text="Create Bunch" />
            </Block>

            <template v-if="isSaving">
                <Block>
                    <p>
                        Your bunch has been created.
                    </p>
                    <p>
                        <CustomLink :url="bunchUrl">Go to bunch!</CustomLink>
                    </p>
                </Block>
            </template>
            <Block v-else>
                <p>
                    <label class="label" for="name">Name</label>
                    <input class="textfield" v-model="name" id="name" type="text">
                </p>
                <p>
                    <label class="label" for="description">Description</label>
                    <input class="textfield" v-model="description" id="description" type="text">
                </p>
                <p>
                    <label class="label" for="currencySymbol">Currency Symbol</label>
                    <input class="textfield" v-model="currencySymbol" id="currencySymbol" type="text">
                </p>
                <p>
                    <label class="label" for="currencyLayout">Currency Layout</label>
                    <CurrencyLayoutDropdown v-model="currencyLayout" :symbol="currencySymbol" />
                </p>
                <p>
                    <label class="label" for="timezone">Timezone</label>
                    <TimezoneDropdown v-model="timezone" />
                </p>
                <p v-if="hasError" class="validation-error">
                    {{errorMessage}}
                </p>
                <p>
                    <CustomButton v-on:click="save" type="action" text="Save" />
                    <CustomButton v-on:click="back" text="Cancel" />
                </p>
            </Block>

        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { TimezoneMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
    import api from '@/api';
    import Layout from '@/components/Layouts/Layout.vue';
    import Block from '@/components/Common/Block.vue';
    import PageHeading from '@/components/Common/PageHeading.vue';
    import PageSection from '@/components/Common/PageSection.vue';
    import CustomButton from '@/components/Common/CustomButton.vue';
    import CustomLink from '@/components/Common/CustomLink.vue';
    import { User } from '@/models/User';
    import { AxiosError } from 'axios';
    import { ApiError} from '@/models/ApiError';
    import { ApiParamsAddBunch } from '@/models/ApiParamsAddBunch';
    import TimezoneDropdown from '@/components/TimezoneDropdown.vue';
    import CurrencyLayoutDropdown from '@/components/CurrencyLayoutDropdown.vue';
    
    @Component({
        components: {
            Layout,
            Block,
            PageHeading,
            PageSection,
            CustomButton,
            CustomLink,
            TimezoneDropdown,
            CurrencyLayoutDropdown
        }
    })
    export default class AddBunchPage extends Mixins(
        TimezoneMixin,
        UserMixin
    ) {
        description = '';
        name = '';
        currencySymbol = '$';
        currencyLayout = '';
        timezone = '';
        errorMessage = '';
        isSaving = false;
        savedSlug = '';

        get hasError(){
            return !!this.errorMessage;
        }

        get loginUrl(){
            return urls.auth.login;
        }

        get bunchUrl(){
            return urls.bunch.details(this.savedSlug);
        }

        async save(){
            this.errorMessage = '';

            try{
                const params: ApiParamsAddBunch = {
                    name: this.name,
                    description: this.description,
                    currencySymbol: this.currencySymbol,
                    currencyLayout: this.currencyLayout,
                    timezone: this.timezone
                };

                const response = await api.addBunch(params);
                this.savedSlug = response.data.id;
                this.isSaving = true;
            } catch (err){
                const error = err as AxiosError<ApiError>;
                const message = error.response?.data.message || 'Unknown Error';
                this.errorMessage = message;
            }
        }

        back(){
            history.back();
        }

        get ready(){
            return this.$_timezonesReady;
        }

        init() {
            this.$_loadTimezones();
        }

        mounted() {
            this.init();
        }

        @Watch('$route')
        routeChanged() {
            this.init();
        }
    }
</script>
