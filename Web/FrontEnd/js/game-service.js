define(["moment"],
    function(moment) {
        "use strict";

        function sortPlayers(players) {
            return players.sort(function (left, right) {
                return getWinnings(right) - getWinnings(left);
            });
        }

        function getStartTime(players) {
            var i, first,
                t = moment().utc(),
                p = players;
            if (p.length === 0)
                return "";
            for (i = 0; i < p.length; i++) {
                first = p[i].checkpoints[0];
                if (first) {
                    var firstTime = moment(first.time);
                    if (firstTime.isBefore(t)) {
                        t = firstTime;
                    }
                }
            }
            return t;
        }

        function canBeEnded(players) {
            var i;
            if (players.length === 0)
                return false;
            for (i = 0; i < players.length; i++) {
                if (!players[i].hasCashedOut) {
                    return false;
                }
            }
            return true;
        }

        function getPlayer(players, playerId) {
            var i;
            for (i = 0; i < players.length; i++) {
                if (players[i].id === playerId) {
                    return players[i];
                }
            }
            return null;
        }

        function getTotalBuyin(players) {
            var sum = 0;
            for (var i = 0; i < players.length; i++) {
                var buyin = 0;
                var player = players[i];
                if (player.checkpoints.length === 0)
                    continue;
                        
                for (var j = 0; j < player.checkpoints.length; j++) {
                    buyin += player.checkpoints[j].addedMoney;
                }
                sum += buyin;
            }
            return sum;
        }

        function getTotalStacks(players) {
            var sum = 0;
            for (var i = 0; i < players.length; i++) {
                var c = players[i].checkpoints;
                sum += c.length > 0 ? c[c.length - 1].stack : 0;
            }
            return sum;
        }

        function getLastReportTime(player) {
            if (player.checkpoints.length === 0)
                return moment().fromNow();
            return moment(player.checkpoints[player.checkpoints.length - 1].time).fromNow();
        }
        
        function getBuyin(player) {
            if (player.checkpoints.length === 0)
                return 0;
            var sum = 0;
            for (var i = 0; i < player.checkpoints.length; i++) {
                sum += player.checkpoints[i].addedMoney;
            }
            return sum;
        }
        
        function getStack(player) {
            var c = player.checkpoints;
            if (c.length === 0)
                return 0;
            return c[c.length - 1].stack;
        }

        function getWinnings(player) {
            return getStack(player) - getBuyin(player);
        }
        
        return {
            sortPlayers: sortPlayers,
            getStartTime: getStartTime,
            canBeEnded: canBeEnded,
            getPlayer: getPlayer,
            getTotalBuyin: getTotalBuyin,
            getTotalStacks: getTotalStacks,
            getLastReportTime: getLastReportTime,
            getBuyin: getBuyin,
            getStack: getStack,
            getWinnings: getWinnings
        }
    }
);