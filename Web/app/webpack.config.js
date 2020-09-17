const webpack = require('webpack');
const path = require('path');
const MiniCssExtractPlugin = require('mini-css-extract-plugin');
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');

module.exports = {
    entry: './js/index.js',
    output: {
        filename: 'dist/[name]-[contenthash].js',
        path: path.resolve(__dirname, '../wwwroot'),
        publicPath: '/'
    },
    devtool: 'source-map',
    module: {
        rules: [
            {
                test: /\.less$/,
                use: [
                    { loader: MiniCssExtractPlugin.loader },
                    { loader: 'css-loader' },
                    { loader: 'less-loader' }
                ],
                exclude: /node_modules/
            },
            {
                test: /\.vue$/,
                loader: 'vue-loader',
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
                    { loader: 'ts-loader' }
                ],
                exclude: /node_modules/
            }
        ]
    },
    plugins: [
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin({
            filename: 'dist/main-[contenthash].css'
        }),
        new VueLoaderPlugin(),
        new webpack.IgnorePlugin(/^\.\/locale$/, /moment$/),
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
    ],
    resolve: {
        alias: {
            vue: 'vue/dist/vue.esm.js',
            '@': path.resolve(__dirname, './js')
        },
        extensions: ['.ts', '.js']
    },
    optimization: {
        splitChunks: {
            chunks: 'all',
            name: 'vendor'
        }
    },
    stats: { children: false }
};
