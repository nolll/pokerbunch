<template>
    <div>
        <h2 class="module-heading">Clear cache</h2>
        <p>
            <CustomButton text="Clear" type="action" v-on:click="clearCache" />
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
    export default class ClearCache extends Vue{
        message: string | null = null;

        get hasMessage(){
            return !!this.message;
        }

        async clearCache(){
            var response = await api.clearCache();
            this.message = response.data.message;
            setTimeout(this.clearMessage, 3000);
        }

        clearMessage(){
            this.message = null;
        }
    }
</script>
