<template>
    <div>
        <h2 class="module-heading">Send test email</h2>
        <p>
            <CustomButton text="Send" type="action" v-on:click="sendEmail" />
        </p>
        <p v-if="hasMessage">
            {{message}}
        </p>
    </div>
</template>

<script lang="ts">
    import { Component, Vue } from 'vue-property-decorator';
    import api from '@/api'; 
    import CustomButton from '@/components/Common/CustomButton.vue';

    @Component({
        components: {
            CustomButton
        }
    })
    export default class SendEmail extends Vue{
        message: string | null = null;

        get hasMessage(){
            return !!this.message;
        }

        async sendEmail(){
            var response = await api.sendEmail();
            this.message = response.data.message;
            setTimeout(this.clearMessage, 3000);
        }

        clearMessage(){
            this.message = null;
        }
    }
</script>
