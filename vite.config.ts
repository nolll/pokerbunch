import { resolve } from 'path';
import vue from '@vitejs/plugin-vue';
import { defineConfig, loadEnv } from 'vite';

export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');
  return {
    resolve: {
      alias: {
        '@': resolve(__dirname, './js'),
        styles: resolve(__dirname, './css'),
      },
    },
    plugins: [vue()],
    build: {
      rollupOptions: {
        output: {
          manualChunks: (id) => {
            if (id.includes('node_modules')) {
              if (id.includes('chart')) {
                return 'charts';
              }
              if (id.includes('vue')) {
                return 'vue';
              }
              return 'vendors';
            }

            return null;
          },
        },
      },
    },
    server: {
      port: 9010,
      proxy: {
        '/api/': {
          target: env.API_URL,
          rewrite: (path) => path.replace(/^\/api\//, ''),
          changeOrigin: true,
          secure: false,
          ws: true,
        },
      },
    },
  };
});

const manualChunks = (id) => {
  if (id.includes('node_modules')) {
    return 'vendor';
  }

  return null;
};
