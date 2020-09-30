import { Component, Vue, Watch } from 'vue-property-decorator';
import { UserStoreGetters, UserStoreActions } from '@/store/helpers/UserStoreHelpers';
import urls from '@/urls';
import { User } from '@/models/User';

@Component
export class UserMixin extends Vue {
    private _isUserRequired = true;

    protected get $_userReady(): boolean {
        return this.$store.getters[UserStoreGetters.UserReady];
    }

    protected get $_isSignedIn(): boolean {
        return this.$store.getters[UserStoreGetters.IsSignedIn];
    }

    protected get $_usersReady(): boolean {
        return this.$store.getters[UserStoreGetters.UsersReady];
    }

    protected get $_userName(): string {
        return this.$store.getters[UserStoreGetters.UserName];
    }

    protected get $_displayName(): string {
        return this.$store.getters[UserStoreGetters.DisplayName];
    }

    protected get $_isAdmin(): boolean {
        return this.$store.getters[UserStoreGetters.IsAdmin];
    }

    protected get $_users(): User[] {
        return this.$store.getters[UserStoreGetters.Users];
    }

    protected $_requireUser() {
        this._isUserRequired = true;
        this.$store.dispatch(UserStoreActions.LoadCurrentUser);
    }

    protected $_loadCurrentUser() {
        this._isUserRequired = false;
        this.$store.dispatch(UserStoreActions.LoadCurrentUser);
    }

    protected $_loadUsers() {
        this.$store.dispatch(UserStoreActions.LoadUsers);
    }

    @Watch('$_userReady')
    userReadyChanged(isUserReady: boolean) {
        if (isUserReady && this._isUserRequired && !this.$_isSignedIn) {
            this.$router.push(urls.auth.login);
        }
    }

    $store: any;
}
