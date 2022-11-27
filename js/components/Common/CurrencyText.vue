<template>
    <span>{{formattedValue}}</span>
</template>

<script setup lang="ts">
    import useFormatter from '@/composables/useFormatter';
    import { computed } from 'vue';

    const props = defineProps<{
        value: number
    }>();

    var formatter = useFormatter();

    const formattedValue = computed(() => {
        return formatter.formatCurrency(props.value);
    });
</script>
