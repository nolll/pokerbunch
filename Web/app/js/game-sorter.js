import comparer from './comparer';

var sortBy =
    {
        date: 'date',
        playerCount: 'playercount',
        duration: 'duration',
        turnover: 'turnover',
        averageBuyin: 'averagebuyin',
    }

export default {
    sort: function (games, sortOrder) {
        return games.slice().sort(getGamesCompareFunc(sortOrder)).reverse();
    }
};

function getGamesCompareFunc(sortOrder) {
    if (sortOrder === sortBy.playerCount)
        return comparePlayerCount;
    if (sortOrder === sortBy.duration)
        return compareDuration;
    if (sortOrder === sortBy.turnover)
        return compareTurnover;
    if (sortOrder === sortBy.averageBuyin)
        return compareAverageBuyin;
    return compareDate;
}

function compareDate(a, b) {
    return comparer.compare(a.date, b.date);
}

function comparePlayerCount(a, b) {
    return comparer.compare(a.playerCount, b.playerCount);
}

function compareDuration(a, b) {
    return comparer.compare(a.duration, b.duration);
}

function compareTurnover(a, b) {
    return comparer.compare(a.turnover, b.turnover);
}

function compareAverageBuyin(a, b) {
    return comparer.compare(a.averageBuyin, b.averageBuyin);
}
