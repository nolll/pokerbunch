<template>
    <Layout :ready="ready">
        <template slot="top-nav">
            <BunchNavigation />
        </template>

        <PageSection>
            <Block>
                <PageHeading :text="$_bunchName" />
            </Block>

            <template v-if="isEditing">
                <Block v-if="$_isManager">
                    <p>
                        <label class="label" for="description">Description</label>
                        <input class="textfield" v-model="formDescription" id="description" type="text" >
                    </p>
                    <p>
                        <label class="label" for="houseRules">House Rules</label>
                        <textarea class="textfield" v-model="formHouseRules" id="houseRules"></textarea>
                    </p>
                    <p>
                        <label class="label" for="defaultBuyin">Default buyin</label>
                        <input class="textfield" v-model.number="formDefaultBuyin" id="defaultBuyin" type="text">
                    </p>
                    <p>
                        <label class="label" for="timezone">Timezone</label>
                        <TimezoneDropdown v-model="formTimezone" />
                    </p>
                    <p>
                        <label class="label" for="currencySymbol">Currency Symbol</label>
                        <input class="textfield" v-model="formCurrencySymbol" id="currencySymbol" type="text">
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
                    {{$_description}}
                </Block>

                <Block v-if="hasHouseRules"><h2>House Rules</h2></Block>
                <Block v-if="hasHouseRules">
                    <p>
                        {{$_houseRules}}
                    </p>
                </Block>

                <Block><h2>Settings</h2></Block>
                <Block>
                    <ValueList>
                        <ValueListKey>Default Buyin</ValueListKey>
                        <ValueListValue>{{defaultBuyin}}</ValueListValue>
                        <ValueListKey>Timezone</ValueListKey>
                        <ValueListValue>{{timezone}}</ValueListValue>
                        <ValueListKey>Currency Format</ValueListKey>
                        <ValueListValue>{{currencyFormat}}</ValueListValue>
                    </ValueList>
                </Block>

                <Block v-if="$_isManager">
                    <CustomButton @click="showEditForm" text="Edit Bunch" type="action" />
                </Block>
            </template>
        </PageSection>
    </Layout>
</template>

<script lang="ts">
    import { Component, Mixins, Watch } from 'vue-property-decorator';
    import { BunchMixin, FormatMixin, TimezoneMixin, UserMixin } from '@/mixins';
    import urls from '@/urls';
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
    import { ApiParamsGetToken } from '@/models/ApiParamsGetToken';
    import { ApiParamsUpdateBunch } from '@/models/ApiParamsUpdateBunch';
    
    @Component({
        components: {
            Layout,
            BunchNavigation,
            Block,
            PageHeading,
            PageSection,
            CustomButton,
            CurrencyLayoutDropdown,
            TimezoneDropdown,
            ValueList,
            ValueListKey,
            ValueListValue
        }
    })
    export default class BunchDetailsPage extends Mixins(
        BunchMixin,
        FormatMixin,
        TimezoneMixin,
        UserMixin
    ) {
        isEditing = false;
        errorMessage: string | null = null;
        formDescription: string | null = null;
        formHouseRules: string | null = null;
        formDefaultBuyin: number | null = null;
        formTimezone: string | null = null;
        formCurrencySymbol: string | null = null;
        formCurrencyLayout: string | null = null;

        get hasDescription() {
            return !!this.$_description;
        }

        get hasHouseRules() {
            return !!this.$_houseRules;
        }

        get canEdit() {
            return this.$_isManager;
        }

        get editUrl() {
            return urls.bunch.edit(this.$_slug);
        }

        get defaultBuyin(){
            return this.$_bunch?.defaultBuyin;
        }

        get timezone(){
            return this.$_bunch?.timezone;
        }

        get currencyFormat(){
            return this.$_formatCurrency(123);
        }

        get currencySymbol(){
            return this.$_bunch?.currencySymbol;
        }

        get currencyLayout(){
            return this.$_bunch?.currencyLayout;
        }

        private showEditForm(){
            this.formDescription = this.$_bunch.description;
            this.formHouseRules = this.$_bunch.houseRules;
            this.formDefaultBuyin = this.$_bunch.defaultBuyin;
            this.formTimezone = this.$_bunch.timezone;
            this.formCurrencySymbol = this.$_bunch.currencySymbol;
            this.formCurrencyLayout = this.$_bunch.currencyLayout;
            this.isEditing = true;
        }

        private hideEditForm(){
            this.isEditing = false;
        }

        private cancel(){
            this.hideEditForm();
        }

        private async save(){
            this.errorMessage = null;

            if(!this.formDefaultBuyin){
                this.errorMessage = 'Please enter a default buyin';
                return;
            }

            if(!this.formTimezone){
                this.errorMessage = 'Please select a timezone';
                return;
            }

            if(!this.formCurrencySymbol){
                this.errorMessage = 'Please enter a currency symbol';
                return;
            }

            if(!this.formCurrencyLayout){
                this.errorMessage = 'Please select a currency layout';
                return;
            }

            const postData: ApiParamsUpdateBunch = {
                description: this.formDescription,
                houseRules: this.formHouseRules,
                defaultBuyin: this.formDefaultBuyin,
                timezone: this.formTimezone,
                currencySymbol: this.formCurrencySymbol,
                currencyLayout: this.formCurrencyLayout
            };

            await api.updateBunch(this.$_bunch.id, postData);
            this.$_refreshBunch();
            this.hideEditForm();
        }
        
        get ready() {
            return this.$_bunchReady && this.$_timezonesReady;
        }

        init() {
            this.$_requireUser();
            this.$_loadBunch();
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
