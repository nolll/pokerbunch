import { BunchResponse } from '@/response/BunchResponse';
import { InjectionKey, Ref } from 'vue';

export const bunchKey = Symbol() as InjectionKey<Ref<BunchResponse>>;
