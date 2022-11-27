declare module '*.vue' {
  import { DefineComponent } from 'vue';
  const component: DefineComponent<{}, {}, any>;
  export default component;
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
