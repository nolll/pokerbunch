import { resolve } from 'path';
import vue from '@vitejs/plugin-vue';
import { defineConfig, loadEnv } from 'vite';

export default defineConfig(({ mode }) => { 
  const env = loadEnv(mode, process.cwd(), '');
  return {
    resolve: {
      alias: {
        '@': resolve(__dirname, './js'),
      },
    },
    plugins: [vue()],
    server: {
      port: 9010,
      proxy: {
        '/api': {
          target: env.API_URL,
          rewrite: (path) => path.replace(/^\/api/, ''),
          changeOrigin: true,
          secure: false,
          ws: true,
        },
      },
    },
  };
});
