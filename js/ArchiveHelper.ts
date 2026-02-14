import { ArchiveCashgame } from './models/ArchiveCashgame';
import { CashgameListPlayerData } from './models/CashgameListPlayerData';
import { CashgamePlayerData } from './models/CashgamePlayerData';
import { CashgamePlayerSortOrder } from './models/CashgamePlayerSortOrder';
import { diffInMinutes } from './time-functions';
import playerSorter from '@/PlayerSorter';
import { ArchiveCashgamePlayer } from './models/ArchiveCashgamePlayer';
import dayjs from 'dayjs';

export default {
  getPlayers(games: ArchiveCashgame[]) {
    const resultsList = [];
    for (const game of games) {
      const winningPlayerId = getWinningPlayerId(game.players);
      for (const singlePlayerResult of game.players) {
        const isWinner = singlePlayerResult.id === winningPlayerId;
        let playerResult = getPlayerResult(resultsList, singlePlayerResult.id);
        if (!playerResult) {
          playerResult = {
            id: singlePlayerResult.id,
            name: singlePlayerResult.name,
            winnings: 0,
            winrate: 0,
            buyin: 0,
            stack: 0,
            gameResults: [],
            gameCount: 0,
            rank: 0,
            playedTimeInMinutes: 0,
          };
          resultsList.push(playerResult);
        }
        const buyinTime = singlePlayerResult.startTime;
        const updatedTime = singlePlayerResult.updatedTime;
        const timeDiffInMinutes = diffInMinutes(buyinTime, updatedTime);
        const playerGameResult: CashgamePlayerData = {
          gameId: game.id,
          buyin: singlePlayerResult.buyin,
          stack: singlePlayerResult.stack,
          winnings: singlePlayerResult.stack - singlePlayerResult.buyin,
          buyinTime,
          updatedTime,
          playedTimeInMinutes: timeDiffInMinutes,
          isWinner,
          playedThisGame: true,
        };
        playerResult.winnings += playerGameResult.winnings;
        playerResult.buyin += playerGameResult.buyin;
        playerResult.stack += playerGameResult.stack;
        playerResult.gameResults.push(playerGameResult);
        playerResult.gameCount++;
        playerResult.playedTimeInMinutes += playerGameResult.playedTimeInMinutes;
      }
    }
    const sortedPlayers = playerSorter.sort(resultsList, CashgamePlayerSortOrder.Winnings);
    for (let spi = 0; spi < sortedPlayers.length; spi++) {
      const p = sortedPlayers[spi];
      p.rank = spi + 1;
      p.winrate = getWinrate(p.winnings, p.playedTimeInMinutes);
    }
    return fillEmptyGames(sortedPlayers, games);
  },
  getCurrentYear(games: ArchiveCashgame[]) {
    if (games.length > 0) {
      const latestGame = games[0];
      return dayjs(latestGame.startTime).year();
    }
    return undefined;
  },
  getYears(games: ArchiveCashgame[]) {
    const years: number[] = [];
    for (const game of games) {
      const year = dayjs(game.startTime).year();
      if (!years.includes(year)) {
        years.push(year);
      }
    }
    return years;
  },
};

const getPlayerResult = (results: CashgameListPlayerData[], id: string): CashgameListPlayerData | null => {
  for (const playerResult of results) {
    if (playerResult && playerResult.id === id) return playerResult;
  }
  return null;
};

const getWinningPlayerId = (players: ArchiveCashgamePlayer[]) => {
  let winningPlayerId = '';
  let best: number | null = null;
  for (const player of players) {
    const winnings = player.stack - player.buyin;
    if (best === null || winnings > best) {
      winningPlayerId = player.id;
      best = winnings;
    }
  }
  return winningPlayerId;
};

const fillEmptyGames = (resultsList: CashgameListPlayerData[], games: ArchiveCashgame[]) => {
  for (const player of resultsList) {
    const playerGameResults = [];
    for (const game of games) {
      const result = getPlayerGame(player.gameResults, game.id);
      playerGameResults.push(result);
    }
    player.gameResults = playerGameResults;
  }
  return resultsList;
};

const getPlayerGame = (playerGames: CashgamePlayerData[], gameId: string): CashgamePlayerData => {
  const game = playerGames.find((playerGame) => playerGame.gameId === gameId);
  return (
    game ?? {
      gameId,
      buyin: 0,
      stack: 0,
      winnings: 0,
      buyinTime: null,
      updatedTime: null,
      playedTimeInMinutes: 0,
      isWinner: false,
      playedThisGame: false,
    }
  );
};

const getWinrate = (winnings: number, timeInMinutes: number) =>
  timeInMinutes === 0 ? 0 : Math.round((winnings / timeInMinutes) * 60);
