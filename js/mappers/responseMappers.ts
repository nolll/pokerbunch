import { Player } from '@/models/Player';
import { PlayerResponse } from '@/response/PlayerResponse';

export const mapPlayer = (response: PlayerResponse): Player => ({
  id: response.id.toString(),
  name: response.name,
  color: response.color,
  userId: response.userId,
  userName: response.userName,
});
