import { ArchiveCashgame } from './models/ArchiveCashgame';
import { CashgameListPlayerData } from './models/CashgameListPlayerData';
import { CashgamePlayerData } from './models/CashgamePlayerData';
import { CashgamePlayerSortOrder } from './models/CashgamePlayerSortOrder';
import timeFunctions from './time-functions';
import playerSorter from '@/PlayerSorter';
import { ArchiveCashgamePlayer } from './models/ArchiveCashgamePlayer';

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
                        playedTimeInMinutes: 0
                    };
                    resultsList.push(playerResult);
                }
                const buyinTime = singlePlayerResult.startTime;
                const updatedTime = singlePlayerResult.updatedTime;
                const timeDiffInMinutes = timeFunctions.diffInMinutes(buyinTime, updatedTime);
                const playerGameResult: CashgamePlayerData = {
                    gameId: game.id,
                    buyin: singlePlayerResult.buyin,
                    stack: singlePlayerResult.stack,
                    winnings: singlePlayerResult.stack - singlePlayerResult.buyin,
                    buyinTime,
                    updatedTime,
                    playedTimeInMinutes: timeDiffInMinutes,
                    isWinner,
                    playedThisGame: true
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
    }
};

function getPlayerResult(results: CashgameListPlayerData[], id: string): CashgameListPlayerData | null {
    for (const playerResult of results) {
        if (playerResult && playerResult.id === id)
            return playerResult;
    }
    return null;
}

function getWinningPlayerId(players: ArchiveCashgamePlayer[]) {
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
}

function fillEmptyGames(resultsList: CashgameListPlayerData[], games: ArchiveCashgame[]) {
    for (const player of resultsList) {
        const playerGameResults = [];
        for (const game of games) {
            const result = getPlayerGame(player.gameResults, game.id);
            playerGameResults.push(result);
        }
        player.gameResults = playerGameResults;
    }
    return resultsList;
}

function getPlayerGame(playerGames: CashgamePlayerData[], gameId: string): CashgamePlayerData {
    for (const playerGame of playerGames) {
        if (playerGame.gameId === gameId)
            return playerGame;
    }
    return {
        gameId,
        buyin: 0,
        stack: 0,
        winnings: 0,
        buyinTime: null,
        updatedTime: null,
        playedTimeInMinutes: 0,
        isWinner: false,
        playedThisGame: false
    };
}

function getWinrate(winnings: number, timeInMinutes: number) {
    if (timeInMinutes === 0)
        return 0;
    const perMinute = winnings / timeInMinutes;
    return Math.round(perMinute * 60);
}
