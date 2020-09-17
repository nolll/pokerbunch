import moment from 'moment';
import api from '@/api';
import playerCalculator from '@/player-calculator';
import actionTypes from '@/action-types';
import { BunchStoreGetters } from '@/store/helpers/BunchStoreHelpers';
import { CashgameStoreGetters, CashgameStoreActions, CashgameStoreMutations } from '@/store/helpers/CashgameStoreHelpers';

const longRefresh = 30000;

export default {
    namespaced: false,
    state: {
        _slug: '',
        _id: '',
        _isRunning: false,
        _playerId: null,
        _locationId: '',
        _locationName: '',
        _players: [],
        _currentStack: 0,
        _reportFormVisible: false,
        _buyinFormVisible: false,
        _cashoutFormVisible: false,
        _cashgameReady: false
    },
    getters: {
        [CashgameStoreGetters.CashgameId]: state => state._id,
        [CashgameStoreGetters.IsRunning]: state => state._isRunning,
        [CashgameStoreGetters.LocationId]: state => state._locationId,
        [CashgameStoreGetters.LocationName]: state => state._locationName,
        [CashgameStoreGetters.ReportFormVisible]: state => state._reportFormVisible,
        [CashgameStoreGetters.BuyinFormVisible]: state => state._buyinFormVisible,
        [CashgameStoreGetters.CashoutFormVisible]: state => state._cashoutFormVisible,
        [CashgameStoreGetters.Players]: state => state._players,
        [CashgameStoreGetters.HasPlayers]: state => state._players.length > 0,
        [CashgameStoreGetters.PlayerId]: (state, getters, rootState, rootGetters) => {
            if(state._playerId)
                return state._playerId;
            return rootGetters[BunchStoreGetters.PlayerId];
        },
        [CashgameStoreGetters.SortedPlayers]: (state) => {
            return state._players.slice().sort(function (left, right) {
                return playerCalculator.getWinnings(right) - playerCalculator.getWinnings(left);
            });
        },
        [CashgameStoreGetters.GetPlayer]: (state) => (id) => {
            if (!id)
                return null;
            return state._players.find(p => p.id.toString() === id.toString()) || null;
        },
        [CashgameStoreGetters.IsInGame]: (state, getters) => {
            return getters[CashgameStoreGetters.Player] !== null;
        },
        [CashgameStoreGetters.CanReport]: (state, getters) => {
            return getters[CashgameStoreGetters.IsInGame] && !getters[CashgameStoreGetters.HasCashedOut];
        },
        [CashgameStoreGetters.CanBuyin]: (state, getters) => {
            return !getters[CashgameStoreGetters.HasCashedOut];
        },
        [CashgameStoreGetters.CanCashout]: (state, getters) => {
            return getters[CashgameStoreGetters.IsInGame];
        },
        [CashgameStoreGetters.HasCashedOut]: (state, getters) => {
            if (!getters[CashgameStoreGetters.IsInGame])
                return false;
            return playerCalculator.hasCashedOut(getters[CashgameStoreGetters.Player]);
        },
        [CashgameStoreGetters.Player]: (state, getters) => {
            return getters[CashgameStoreGetters.GetPlayer](getters[CashgameStoreGetters.PlayerId]);
        },
        [CashgameStoreGetters.TotalStacks]: state => {
            var sum = 0;
            for (var i = 0; i < state._players.length; i++) {
                var c = state._players[i].actions;
                sum += c.length > 0 ? c[c.length - 1].stack : 0;
            }
            return sum;
        },
        [CashgameStoreGetters.TotalBuyin]: state => {
            var sum = 0;
            for (var i = 0; i < state._players.length; i++) {
                var buyin = 0;
                var player = state._players[i];
                if (player.actions.length === 0)
                    continue;

                for (var j = 0; j < player.actions.length; j++) {
                    buyin += player.actions[j].added || 0;
                }
                sum += buyin;
            }
            return sum;
        },
        [CashgameStoreGetters.StartTime]: state => {
            var i,
                first,
                t = moment().utc(),
                p = state._players;

            if (p.length === 0)
                return t;
            for (i = 0; i < p.length; i++) {
                first = p[i].actions[0];
                if (first) {
                    const firstTime = moment(first.time);
                    if (firstTime.isBefore(t)) {
                        t = firstTime;
                    }
                }
            }
            return t;
        },
        [CashgameStoreGetters.UpdatedTime]: state => {
            return moment(state._updatedTime);
        },
        [CashgameStoreGetters.CashgameReady](state) {
            return state._cashgameReady;
        }
    },
    actions: {
        [CashgameStoreActions.LoadCashgame](context, { id }) {
            api.getCashgame(id)
                .then(function (response) {
                    if (response.status === 200) {
                        context.commit(CashgameStoreMutations.DataLoaded, response.data);
                    }
                    context.commit(CashgameStoreMutations.LoadingComplete);
                    setupRefresh(context, longRefresh);
                })
                .catch(function () {
                    
                });
        },
        [CashgameStoreActions.SelectPlayer](context, { playerId }) {
            context.commit(CashgameStoreMutations.SetPlayerId, playerId);
        },
        [CashgameStoreActions.Refresh](context) {
            refresh(context);
        },
        [CashgameStoreActions.Report](context, { stack }) {
            const player = context.getters[CashgameStoreGetters.Player];
            const reportData = { type: 'report', playerId: player.id, stack: stack };
            api.report(context.state._id, reportData)
                .then(function() {
                    refresh(context);
                });
            context.commit(CashgameStoreMutations.Report, { player, reportData });
            context.commit(CashgameStoreMutations.HideForms);
            context.commit(CashgameStoreMutations.ResetPlayerId);
        },
        [CashgameStoreActions.Buyin](context, { amount, stack }) {
            const buyinData = { type: 'buyin', playerId: context.getters[CashgameStoreGetters.PlayerId], stack: stack, added: amount };
            const player = context.getters[CashgameStoreGetters.Player];
            api.buyin(context.state._id, buyinData)
                .then(function () {
                    refresh(context);
                });
            context.commit(CashgameStoreMutations.Buyin, { player, buyinData });
            context.commit(CashgameStoreMutations.HideForms);
            context.commit(CashgameStoreMutations.ResetPlayerId);
        },
        [CashgameStoreActions.FirstBuyin](context, { amount, stack, name, color }) {
            const player = createPlayer(context.getters[CashgameStoreGetters.PlayerId], name, color);
            context.commit(CashgameStoreMutations.AddPlayer, { player });
            context.dispatch(CashgameStoreActions.Buyin, { amount: amount, stack: stack });
        },
        [CashgameStoreActions.Cashout](context, { stack }) {
            const player = context.getters[CashgameStoreGetters.Player];
            const cashoutData = { type: 'cashout', playerId: player.id, stack: stack };
            api.cashout(context.state._id, cashoutData)
                .then(function () {
                    refresh(context);
                });
            context.commit(CashgameStoreMutations.Cashout, { player, cashoutData });
            context.commit(CashgameStoreMutations.HideForms);
            context.commit(CashgameStoreMutations.ResetPlayerId);
        },
        [CashgameStoreActions.ShowReportForm](context) {
            refresh(context);
            context.commit(CashgameStoreMutations.ShowReportForm);
        },
        [CashgameStoreActions.ShowBuyinForm](context) {
            refresh(context);
            context.commit(CashgameStoreMutations.ShowBuyinForm);
        },
        [CashgameStoreActions.ShowCashoutForm](context) {
            refresh(context);
            context.commit(CashgameStoreMutations.ShowCashoutForm);
        },
        [CashgameStoreActions.HideForms](context) {
            context.commit(CashgameStoreMutations.HideForms);
        },
        [CashgameStoreActions.DeleteAction](context, data) {
            context.commit(CashgameStoreMutations.RemoveAction, data);
            api.deleteAction(context.state._id, data.id)
                .then(function () {
                    refresh(context);
                });
        },
        [CashgameStoreActions.SaveAction](context, data) {
            context.commit(CashgameStoreMutations.UpdateAction, data);
            const updateData = {
                added: data.added,
                stack: data.stack,
                timestamp: data.time
            };
            api.updateAction(context.state._id, data.id, updateData)
                .then(function () {
                    refresh(context);
                });
        }
    },
    mutations: {
        [CashgameStoreMutations.SetPlayerId](state, playerId) {
            state._playerId = playerId;
        },
        [CashgameStoreMutations.ResetPlayerId](state) {
            state._playerId = null;
        },
        [CashgameStoreMutations.SetPlayers](state, players) {
            state._players = players;
        },
        [CashgameStoreMutations.SetIsRunning](state, isRunning) {
            state._isRunning = isRunning;
        },
        [CashgameStoreMutations.Report](state, { player, reportData }) {
            state._currentStack = reportData.stack;
            addCheckpoint(actionTypes.report, player, reportData.stack, 0);
        },
        [CashgameStoreMutations.Cashout](state, { player, cashoutData }) {
            state._currentStack = cashoutData.stack;
            addCheckpoint(actionTypes.cashout, player, cashoutData.stack, 0);
        },
        [CashgameStoreMutations.Buyin](state, { player, buyinData }) {
            const afterStack = buyinData.stack + buyinData.added;
            state._currentStack = afterStack;
            addCheckpoint(actionTypes.buyin, player, afterStack, buyinData.added);
        },
        [CashgameStoreMutations.AddPlayer](state, { player }) {
            state._players.push(player);
        },
        [CashgameStoreMutations.ShowReportForm](state) {
            state._reportFormVisible = true;
        },
        [CashgameStoreMutations.ShowBuyinForm](state) {
            state._buyinFormVisible = true;
        },
        [CashgameStoreMutations.ShowCashoutForm](state) {
            state._cashoutFormVisible = true;
        },
        [CashgameStoreMutations.HideForms](state) {
            state._reportFormVisible = false;
            state._buyinFormVisible = false;
            state._cashoutFormVisible = false;
        },
        [CashgameStoreMutations.LoadingComplete](state) {
            state._cashgameReady = true;
        },
        [CashgameStoreMutations.RemoveAction](state, payload) {
            state._players.forEach(function (player) {
                const p = player.actions.map(item => item.id).indexOf(payload.id);
                if (p !== -1) {
                    player.actions.splice(p, 1);
                }
            });
        },
        [CashgameStoreMutations.UpdateAction](state, payload) {
            state._players.forEach(function (player) {
                player.actions = player.actions.map(action => {
                    if (action.id === payload.id) {
                        return Object.assign({}, action, payload.data);
                    }
                    return action;
                });
            });
        },
        [CashgameStoreMutations.DataLoaded](state, data) {
            state._isRunning = data.isRunning;
            state._id = data.id;
            state._slug = data.bunch.id;
            state._locationId = data.location.id;
            state._locationName = data.location.name;
            state._updatedTime = data.updatedTime;
            state._players = data.players;
        }
    }
};

function setupRefresh(context, refreshTimeout) {
    if (context.state._isRunning) {
        window.setTimeout(function () {
            refresh(context);
        }, refreshTimeout);
    }
}

function refresh(context) {
    api.getCashgame(context.state._id)
        .then(function (response) {
            if (response.status === 200) {
                setPlayers(context, response.data);
            }
        })
        .catch(function(response) {
            console.log('refresh error');
        });
}

function setPlayers(context, data) {
    context.commit(CashgameStoreMutations.SetPlayers, data.players);
    context.commit(CashgameStoreMutations.SetIsRunning, data.isRunning);
    setupRefresh(context, longRefresh);
}

function addCheckpoint(type, player, stack, added) {
    const action = { type: type, time: moment().utc().format(), stack: stack, added: added };
    player.actions.push(action);
}

function createPlayer(playerId, playerName, playerColor) {
    return {
        id: playerId,
        name: playerName || '',
        color: playerColor || '#9e9e9e',
        url: null,
        actions: []
    };
}
