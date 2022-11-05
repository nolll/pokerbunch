const config = window.vueConfig;

if (!config) {
    throw new Error('No api host configured');
}

export default {
    get apiUrl() {
        return config.apiUrl;
    }
};
