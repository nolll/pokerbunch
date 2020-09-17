export enum PlayerStoreGetters {
    Slug = 'player_slug',
    Players = 'player_players',
    PlayersReady = 'player_playersReady',
    GetPlayer = 'player_getPlayer'
}

export enum PlayerStoreActions {
    LoadPlayers = 'player_loadPlayers'
}

export enum PlayerStoreMutations {
    SetPlayersData = 'player_setPlayersData',
    SetInitialized = 'player_setInitialized'
}