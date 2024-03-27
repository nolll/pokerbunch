const webpack = require('webpack');
const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const { VueLoaderPlugin } = require('vue-loader');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const CssMinimizerPlugin = require('css-minimizer-webpack-plugin');

const getMode = () => {
  return isDev() ? 'development' : 'production';
};

function getEntry() {
  return './js/index.ts';
}

function getOutput() {
  return {
    filename: getJsFilename(),
    path: path.resolve(__dirname, './dist'),
    publicPath: '/',
  };
}

function getDevtool() {
  return 'source-map';
}

function getModule() {
  return {
    rules: [
      {
        test: /\.s[ac]ss$/i,
        use: [{ loader: MiniCssExtractPlugin.loader }, { loader: 'css-loader' }, { loader: 'sass-loader' }],
        exclude: /node_modules/,
      },
      {
        test: /\.vue$/,
        use: [
          {
            loader: 'vue-loader',
          },
        ],
        exclude: /node_modules/,
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/,
      },
      {
        test: /\.ts$/,
        use: [
          { loader: 'babel-loader' },
          {
            loader: 'ts-loader',
            options: {
              appendTsSuffixTo: [/\.vue$/],
            },
          },
        ],
        exclude: /node_modules/,
      },
    ],
  };
}

function getJsFilename() {
  return isDev() ? 'dist/[name].js' : 'dist/[name]-[contenthash:7].js';
}

function getCssFilename() {
  return isDev() ? 'dist/main.css' : 'dist/main-[contenthash:7].css';
}

function getPlugins() {
  const plugins = [
    new webpack.DefinePlugin({
      __VUE_OPTIONS_API__: false,
      __VUE_PROD_DEVTOOLS__: true,
    }),
    new CleanWebpackPlugin(),
    new MiniCssExtractPlugin({
      filename: getCssFilename(),
    }),
    new VueLoaderPlugin(),
    new CopyPlugin({
      patterns: [{ from: './favicon.ico', to: '.' }],
    }),
    new HtmlWebpackPlugin({
      template: path.resolve(__dirname, './index.html'),
    }),
  ];

  if (isAnalyzing()) {
    const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
    plugins.push(new BundleAnalyzerPlugin());
  }

  return plugins;
}

function getResolve() {
  return {
    alias: {
      '@': path.resolve(__dirname, './js'),
    },
    extensions: ['.ts', '.js', '.vue'],
  };
}

function getStats() {
  return { children: false };
}

function getOptimization() {
  return isDev()
    ? {}
    : {
        splitChunks: {
          chunks: 'all',
          name: 'vendor',
        },
        minimizer: [new CssMinimizerPlugin()],
      };
}

function getDevServer() {
  return isDev()
    ? {
        static: {
          directory: path.join(__dirname, 'dist'),
        },
        compress: true,
        port: 9000,
        server: {
          type: 'https',
        },
        proxy: [
          {
            context: ['/api/'],
            //target: 'https://pokerbunch-api.herokuapp.com',
            target: 'https://localhost:44315',
            pathRewrite: { '^/api': '' },
            secure: false,
            changeOrigin: true,
          },
        ],
        historyApiFallback: true,
      }
    : {};
}

function isDev() {
  return process.env.NODE_ENV === 'dev';
}

function isAnalyzing() {
  return process.env.ANALYZE_BUNDLE === '1';
}

module.exports = {
  mode: getMode(),
  entry: getEntry(),
  output: getOutput(),
  devtool: getDevtool(),
  module: getModule(),
  plugins: getPlugins(),
  resolve: getResolve(),
  stats: getStats(),
  optimization: getOptimization(),
  devServer: getDevServer(),
};
