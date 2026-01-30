import { resolve } from 'path';
import vue from '@vitejs/plugin-vue';

function getProxy() {
  return {
    '/api': {
      target: 'https://api.pokerbunch.com',
      //target: 'https://localhost:5010',
      rewrite: (path) => path.replace(/^\/api/, ''),
      changeOrigin: true,
      secure: false,
      ws: true,
    },
  };
}

export default {
  resolve: {
    alias: {
      '@': resolve(__dirname, './js'),
    },
  },
  plugins: [vue()],
  server: {
    port: 9010,
    proxy: getProxy(),
  },
};
