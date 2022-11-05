import { Player } from '@/models/Player';

export enum PlayerStoreMutations {
  SetPlayersData = 'player_setPlayersData',
  SetSlug = 'player_setSlug',
  AddPlayer = 'player_addPlayer',
  DeletePlayer = 'player_deletePlayer',
}

export interface PlayerStoreState {
  _slug: string;
  _players: Player[];
  _playersReady: boolean;
}
