const { merge } = require('webpack-merge');
const common = require('./webpack.config.js');
var OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');

module.exports = merge(common, {
    mode: 'production',
    plugins: getPlugins(),
    devtool: 'source-map'
});

function getPlugins() {
    const plugins = [
        new OptimizeCssAssetsPlugin()
    ];

    if (process.env.ANALYZE_BUNDLE) {
        const BundleAnalyzerPlugin = require('webpack-bundle-analyzer').BundleAnalyzerPlugin;

        plugins.push(new BundleAnalyzerPlugin());
    }

    return plugins;
}