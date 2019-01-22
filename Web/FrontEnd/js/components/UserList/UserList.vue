<template>
    <simple-list>
        <simple-list-item v-for="user in users" :key="user.id">
            <user-list-item :userName="user.userName" />
        </simple-list-item>
    </simple-list>
</template>

<script>
    import { mapGetters } from 'vuex';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import UserListItem from '@/components/UserList/UserListItem.vue';
    import { USER } from '@/store-names';

    export default {
        components: {
            SimpleList,
            SimpleListItem,
            UserListItem
        },
        computed: {
            ...mapGetters(USER, [
                'users'
            ])
        }
    };
</script>

<style>

</style>
