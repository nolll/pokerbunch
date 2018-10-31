import comparer from './comparer';

var sortBy =
    {
        winnings: 'winnings',
        buyin: 'buyin',
        stack: 'stack',
        time: 'time',
        gameCount: 'gamecount',
        winrate: 'winrate',
    }

export default {
    sort: function (players, sortOrder) {
        return players.slice().sort(getPlayersCompareFunc(sortOrder)).reverse();
    }
};

function getPlayersCompareFunc(sortOrder) {
    if (sortOrder === sortBy.buyin)
        return compareBuyin;
    if (sortOrder === sortBy.stack)
        return compareStack;
    if (sortOrder === sortBy.time)
        return compareTime;
    if (sortOrder === sortBy.gameCount)
        return compareGameCount;
    if (sortOrder === sortBy.winrate)
        return compareWinrate;
    return compareWinnings;
}

function compareWinnings(a, b) {
    return comparer.compare(a.winnings, b.winnings);
}

function compareBuyin(a, b) {
    return comparer.compare(a.buyin, b.buyin);
}

function compareStack(a, b) {
    return comparer.compare(a.stack, b.stack);
}

function compareTime(a, b) {
    return comparer.compare(a.playedTimeInMinutes, b.playedTimeInMinutes);
}

function compareGameCount(a, b) {
    return comparer.compare(a.gameCount, b.gameCount);
}

function compareWinrate(a, b) {
    return comparer.compare(a.winrate, b.winrate);
}
