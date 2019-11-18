const merge = require('webpack-merge');
const common = require('./webpack.config.js');
var OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');

module.exports = merge(common, {
    mode: 'production',
    plugins: [
        new OptimizeCssAssetsPlugin()
    ],
    devtool: 'source-map'
});