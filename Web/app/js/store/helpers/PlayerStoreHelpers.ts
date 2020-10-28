import { Player } from '@/models/Player';

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
    SetSlug = 'player_setSlug'
}

export interface PlayerStoreState {
    _slug: string;
    _players: Player[];
    _playersReady: boolean;
}
