import { UserStoreGetters, UserStoreActions } from '@/store/helpers/UserStoreHelpers';
import urls from '@/urls';

export default {
    data: function () {
        return {
            _isUserRequired: true
        };
    },
    computed: {
        $_userReady() {
            return this.$store.getters[UserStoreGetters.UserReady];
        },
        $_isSignedIn() {
            return this.$store.getters[UserStoreGetters.IsSignedIn];
        },
        $_usersReady() {
            return this.$store.getters[UserStoreGetters.UsersReady];
        },
        $_userName() {
            return this.$store.getters[UserStoreGetters.UserName];
        },
        $_displayName() {
            return this.$store.getters[UserStoreGetters.DisplayName];
        },
        $_isAdmin() {
            return this.$store.getters[UserStoreGetters.IsAdmin];
        },
        $_users() {
            return this.$store.getters[UserStoreGetters.Users];
        }
    },
    methods: {
        $_requireUser() {
            this._isUserRequired = true;
            this.$store.dispatch(UserStoreActions.LoadUser);
        },
        $_loadUser() {
            this._isUserRequired = false;
            this.$store.dispatch(UserStoreActions.LoadUser);
        },
        $_loadUsers() {
            this.$store.dispatch(UserStoreActions.LoadUsers);
        }
    },
    watch: {
        $_userReady: function (isUserReady) {
            if (isUserReady && this._isUserRequired && !this.$_isSignedIn) {
                this.$router.push(urls.auth.login);
            }
        }
    }
};
