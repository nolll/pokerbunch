<template>
    <SimpleList>
        <SimpleListItem v-for="user in users" :key="user.id">
            <UserList-item :userName="user.userName" />
        </SimpleListItem>
    </SimpleList>
</template>

<script lang="ts">
    import { Component, Mixins } from 'vue-property-decorator';
    import SimpleList from '@/components/Common/SimpleList/SimpleList.vue';
    import SimpleListItem from '@/components/Common/SimpleList/SimpleListItem.vue';
    import UserListItem from '@/components/UserList/UserListItem.vue';
    import { UserMixin } from '@/mixins';

    @Component({
        components: {
            SimpleList,
            SimpleListItem,
            UserListItem
        }
    })
    export default class UserList extends Mixins(
        UserMixin
    ) {
        get users(){
            return this.$_users;
        }
    }
</script>
