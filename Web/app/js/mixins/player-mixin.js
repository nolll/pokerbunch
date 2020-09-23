﻿import { PlayerStoreGetters, PlayerStoreActions } from '@/store/helpers/PlayerStoreHelpers';

export default {
    computed: {
        $_playersReady() {
            return this.$store.getters[PlayerStoreGetters.PlayersReady];
        },
        $_players() {
            return this.$store.getters[PlayerStoreGetters.Players];
        }
    },
    methods: {
        $_loadPlayers() {
            this.$store.dispatch(PlayerStoreActions.LoadPlayers, { slug: this.$route.params.slug });
        },
        $_getPlayer(id) {
            return this.$store.getters[PlayerStoreGetters.GetPlayer](id);
        }
    }
};
