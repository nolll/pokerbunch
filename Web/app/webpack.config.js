const webpack = require('webpack');
const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const VueLoaderPlugin = require('vue-loader/lib/plugin');
const HtmlWebpackPlugin = require('html-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const CopyPlugin = require('copy-webpack-plugin');

module.exports = {
    entry: './js/index.ts',
    output: {
        filename: 'dist/[name]-[contenthash].js',
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
                    loader: 'vue-loader',
                    options: {
                        appendExtension: true
                    }
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
    plugins: [
        new CleanWebpackPlugin(),
        new MiniCssExtractPlugin({
            filename: 'dist/main-[contenthash].css'
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
    ],
    resolve: {
        alias: {
            '@': path.resolve(__dirname, './js')
        },
        extensions: ['.ts', '.js', '.vue']
    },
    stats: { children: false }
};
