const webpack = require('webpack');
const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const { VueLoaderPlugin } = require('vue-loader');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');
const CssMinimizerPlugin = require("css-minimizer-webpack-plugin");

module.exports = {
    mode: getMode(),
    entry: './js/index.ts',
    output: {
        filename: getJsFilename(),
        path: path.resolve(__dirname, '../wwwroot'),
        publicPath: '/'
    },
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.s[ac]ss$/i,
                use: [
                    { loader: MiniCssExtractPlugin.loader },
                    { loader: 'css-loader' },
                    { loader: 'sass-loader' }
                ],
                exclude: /node_modules/
            },
            {
                test: /\.vue$/,
                use: [{
                    loader: 'vue-loader'
                }],
                exclude: /node_modules/
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /node_modules/
            },
            {
                test: /\.ts$/,
                use: [
                    { loader: 'babel-loader' },
                    {
                        loader: 'ts-loader',
                        options: {
                            appendTsSuffixTo: [/\.vue$/]
                        }
                    }
                ],
                exclude: /node_modules/
            }
        ]
    },
    plugins: getPlugins(),
    resolve: {
        alias: {
            '@': path.resolve(__dirname, './js')
        },
        extensions: ['.ts', '.js', '.vue']
    },
    stats: { children: false },
    optimization: getOptimization()
};

function getMode(){
    return isDev()
        ? 'development'
        : 'production';
}

function getJsFilename(){
    return isDev()
        ? 'dist/[name].js'
        : 'dist/[name]-[contenthash].js';
}

function getCssFilename(){
    return isDev()
        ? 'dist/main.css'
        : 'dist/main-[contenthash].css';
}

function getPlugins() {
    const plugins = [
        new webpack.DefinePlugin({
            __VUE_OPTIONS_API__: false,
            __VUE_PROD_DEVTOOLS__: true
        }),
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin({
            filename: getCssFilename()
        }),
        new VueLoaderPlugin(),
        new HtmlWebpackPlugin({
            filename: path.resolve(__dirname, '../Views/Generated/Script.cshtml'),
            template: path.resolve(__dirname, './templates/ScriptTemplate.txt'),
            inject: false
        }),
        new HtmlWebpackPlugin({
            filename: path.resolve(__dirname, '../Views/Generated/Style.cshtml'),
            template: path.resolve(__dirname, './templates/StyleTemplate.txt'),
            inject: false
        }),
        new CopyPlugin({
            patterns: [
                { from: './fonts/*.*', to: './dist' },
                { from: './favicon.ico', to: '.' }
            ]
        })
    ];

    if (isAnalyzing()) {
        const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;
        plugins.push(new BundleAnalyzerPlugin());
    }

    return plugins;
}

function getOptimization(){
    return isDev()
        ? {}
        : {
            splitChunks: {
                chunks: 'all',
                name: 'vendor'
            },
            minimizer: [
                new CssMinimizerPlugin(),
            ]
        }
}

function isDev(){
    return process.env.NODE_ENV === 'dev';
}

function isAnalyzing(){
    return process.env.ANALYZE_BUNDLE === '1';
}