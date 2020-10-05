declare module '*.vue' {
  import Vue from 'vue';
  export default Vue;
}

declare module 'vuedraggable';

declare module 'google-charts';

declare module '*.js';

interface IVueConfig {
  apiUrl: string;
}

interface Window {
  vueConfig: IVueConfig;
}

