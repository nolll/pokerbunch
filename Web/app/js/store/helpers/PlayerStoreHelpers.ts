import { Player } from '@/models/Player';

export enum PlayerStoreGetters {
    Slug = 'player_slug',
    Players = 'player_players',
    PlayersReady = 'player_playersReady',
    GetPlayer = 'player_getPlayer'
}

export enum PlayerStoreActions {
    LoadPlayers = 'player_loadPlayers',
    AddPlayer = 'player_addPlayer',
    DeletePlayer = 'player_deletePlayer'
}

export enum PlayerStoreMutations {
    SetPlayersData = 'player_setPlayersData',
    SetSlug = 'player_setSlug',
    AddPlayer = 'player_addPlayer',
    DeletePlayer = 'player_deletePlayer'
}

export interface PlayerStoreState {
    _slug: string;
    _players: Player[];
    _playersReady: boolean;
}

export interface AddPlayerParams{
    bunchId: string;
    name: string;
}

export interface DeletePlayerParams{
    player: Player;
}
