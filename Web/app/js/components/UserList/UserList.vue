<template>
    <simple-list>
        <simple-list-item v-for="user in $_users" :key="user.id">
            <user-list-item :userName="user.userName" />
        </simple-list-item>
    </simple-list>
</template>

<script>
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import UserListItem from '@/components/UserList/UserListItem.vue';
    import { UserMixin } from '@/mixins';

    export default {
        components: {
            SimpleList,
            SimpleListItem,
            UserListItem
        },
        mixins: [
            UserMixin
        ]
    };
</script>

<style>

</style>
