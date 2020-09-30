import { Component, Vue } from 'vue-property-decorator';
import { CurrentGameStoreGetters, CurrentGameStoreActions } from '@/store/helpers/CurrentGameStoreHelpers';
import { CurrentGameResponse } from '@/response/CurrentGameResponse';

@Component
export class CurrentGameMixin extends Vue {
    protected get $_currentGames(): CurrentGameResponse[] {
        return this.$store.getters[CurrentGameStoreGetters.CurrentGames];
    }

    protected get $_currentGamesReady(): boolean {
        return this.$store.getters[CurrentGameStoreGetters.CurrentGamesReady];
    }

    protected $_loadCurrentGames() {
        this.$store.dispatch(CurrentGameStoreActions.LoadCurrentGames, { slug: this.$route.params.slug });
    }

    $store: any;
}
