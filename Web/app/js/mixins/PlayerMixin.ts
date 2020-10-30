import { Component, Vue } from 'vue-property-decorator';
import { PlayerStoreGetters, PlayerStoreActions } from '@/store/helpers/PlayerStoreHelpers';
import { Player } from '@/models/Player';

@Component
export class PlayerMixin extends Vue {
    protected get $_playersReady() : boolean {
        return this.$store.getters[PlayerStoreGetters.PlayersReady];
    }

    protected get $_players(): Player[] {
        return this.$store.getters[PlayerStoreGetters.Players];
    }

    protected $_getPlayer(id: string): Player {
        return this.$store.getters[PlayerStoreGetters.GetPlayer](id);
    }

    protected $_loadPlayers() {
        this.$store.dispatch(PlayerStoreActions.LoadPlayers, { slug: this.$route.params.slug });
    }

    protected $_addPlayer(name: string) {
        this.$store.dispatch(PlayerStoreActions.AddPlayer, { bunchId: this.$route.params.slug, name });
    }

    $store: any;
}
