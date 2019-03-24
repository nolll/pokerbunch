var config = window.vueConfig;

if (!config) {
    throw new Error('No api host configured');
}

export default {
    get apiHost() {
        return config.apiHost;
    }
};
