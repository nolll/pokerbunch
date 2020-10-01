import { ActionContext, StoreOptions } from 'vuex';
import api from '@/api';
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import playerCalculator from '@/PlayerCalculator';
import { BunchStoreGetters } from '@/store/helpers/BunchStoreHelpers';
import { CashgameStoreGetters, CashgameStoreActions, CashgameStoreMutations, CashgameStoreState, BuyinParams, FirstBuyinParams, DeleteActionParams } from '@/store/helpers/CashgameStoreHelpers';
import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
import { DetailedCashgameResponseActionType } from '@/response/DetailedCashgameResponseActionType';
import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';
import { DetailedCashgame } from '@/models/DetailedCashgame';
import { DetailedCashgameResponseAction } from '@/response/DetailedCashgameResponseAction';
import { DetailedCashgamePlayer } from '@/models/DetailedCashgamePlayer';
import { DetailedCashgameAction } from '@/models/DetailedCashgameAction';

dayjs.extend(utc);
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
        _updatedTime: null,
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
            return state._players.slice().sort((left, right) => playerCalculator.getWinnings(right) - playerCalculator.getWinnings(left));
        },
        [CashgameStoreGetters.GetPlayer]: (state) => (id: string) => {
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
            let sum = 0;
            for (let i = 0; i < state._players.length; i++) {
                const c = state._players[i].actions;
                sum += c.length > 0 ? c[c.length - 1].stack : 0;
            }
            return sum;
        },
        [CashgameStoreGetters.TotalBuyin]: state => {
            let sum = 0;
            for (let i = 0; i < state._players.length; i++) {
                let buyin = 0;
                const player = state._players[i];
                if (player.actions.length === 0)
                    continue;

                for (let j = 0; j < player.actions.length; j++) {
                    buyin += player.actions[j].added || 0;
                }
                sum += buyin;
            }
            return sum;
        },
        [CashgameStoreGetters.StartTime]: state => {
            let first;
            let t = dayjs.utc();
            const p = state._players;

            if (p.length === 0)
                return t;
            for (let i = 0; i < p.length; i++) {
                first = p[i].actions[0];
                if (first) {
                    const firstTime = dayjs(first.time);
                    if (firstTime.isBefore(t)) {
                        t = firstTime;
                    }
                }
            }
            return t.toDate();
        },
        [CashgameStoreGetters.UpdatedTime]: state => {
            return state._updatedTime;
        },
        [CashgameStoreGetters.CashgameReady](state) {
            return state._cashgameReady;
        }
    },
    actions: {
        async [CashgameStoreActions.LoadCashgame](context, { id }) {
            const response = await api.getCashgame(id);

            if (response.status === 200) {
                const cashgame = mapCashgame(response.data);
                context.commit(CashgameStoreMutations.DataLoaded, cashgame);
            }
            context.commit(CashgameStoreMutations.LoadingComplete);
            setupRefresh(context, longRefresh);
        },
        [CashgameStoreActions.SelectPlayer](context, { playerId }) {
            context.commit(CashgameStoreMutations.SetPlayerId, playerId);
        },
        async [CashgameStoreActions.Refresh](context) {
            await refresh(context);
        },
        async [CashgameStoreActions.Report](context, { stack }) {
            const player = context.getters[CashgameStoreGetters.Player];
            const reportData = { type: 'report', playerId: player.id, stack };
            await api.report(context.state._id, reportData);
            await refresh(context);
            context.commit(CashgameStoreMutations.Report, { player, reportData });
            context.commit(CashgameStoreMutations.HideForms);
            context.commit(CashgameStoreMutations.ResetPlayerId);
        },
        async [CashgameStoreActions.Buyin](context, params: BuyinParams) {
            const buyinData = { type: 'buyin', playerId: context.getters[CashgameStoreGetters.PlayerId], stack: params.stack, added: params.amount };
            const player = context.getters[CashgameStoreGetters.Player];
            await api.buyin(context.state._id, buyinData);
            await refresh(context);
            context.commit(CashgameStoreMutations.Buyin, { player, buyinData });
            context.commit(CashgameStoreMutations.HideForms);
            context.commit(CashgameStoreMutations.ResetPlayerId);
        },
        async [CashgameStoreActions.FirstBuyin](context, params: FirstBuyinParams) {
            const player = createPlayer(context.getters[CashgameStoreGetters.PlayerId], params.name, params.color);
            context.commit(CashgameStoreMutations.AddPlayer, { player });
            context.dispatch(CashgameStoreActions.Buyin, { amount: params.amount, stack: params.stack });
        },
        async [CashgameStoreActions.Cashout](context, { stack }) {
            const player = context.getters[CashgameStoreGetters.Player];
            const cashoutData = { type: 'cashout', playerId: player.id, stack };
            await api.cashout(context.state._id, cashoutData);
            await refresh(context);
            context.commit(CashgameStoreMutations.Cashout, { player, cashoutData });
            context.commit(CashgameStoreMutations.HideForms);
            context.commit(CashgameStoreMutations.ResetPlayerId);
        },
        async [CashgameStoreActions.ShowReportForm](context) {
            await refresh(context);
            context.commit(CashgameStoreMutations.ShowReportForm);
        },
        async [CashgameStoreActions.ShowBuyinForm](context) {
            await refresh(context);
            context.commit(CashgameStoreMutations.ShowBuyinForm);
        },
        async [CashgameStoreActions.ShowCashoutForm](context) {
            await refresh(context);
            context.commit(CashgameStoreMutations.ShowCashoutForm);
        },
        [CashgameStoreActions.HideForms](context) {
            context.commit(CashgameStoreMutations.HideForms);
        },
        async [CashgameStoreActions.DeleteAction](context, params: DeleteActionParams) {
            context.commit(CashgameStoreMutations.RemoveAction, params);
            await api.deleteAction(context.state._id, params.id);
            await refresh(context);
        },
        async [CashgameStoreActions.SaveAction](context, data) {
            context.commit(CashgameStoreMutations.UpdateAction, data);
            const updateData = {
                added: data.added,
                stack: data.stack,
                timestamp: data.time
            };
            await api.updateAction(context.state._id, data.id, updateData);
            await refresh(context);
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
            addCheckpoint(DetailedCashgameResponseActionType.Report, player, reportData.stack, 0);
        },
        [CashgameStoreMutations.Cashout](state, { player, cashoutData }) {
            state._currentStack = cashoutData.stack;
            addCheckpoint(DetailedCashgameResponseActionType.Cashout, player, cashoutData.stack, 0);
        },
        [CashgameStoreMutations.Buyin](state, { player, buyinData }) {
            const afterStack = buyinData.stack + buyinData.added;
            state._currentStack = afterStack;
            addCheckpoint(DetailedCashgameResponseActionType.Buyin, player, afterStack, buyinData.added);
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
        [CashgameStoreMutations.RemoveAction](state, params: DeleteActionParams) {
            state._players.forEach((player) => {
                    const p = player.actions.map(item => item.id).indexOf(params.id);
                    if (p !== -1) {
                        player.actions.splice(p, 1);
                    }
                });
        },
        [CashgameStoreMutations.UpdateAction](state, payload) {
            state._players.forEach((player) => {
                    player.actions = player.actions.map(action => {
                        if (action.id === payload.id) {
                            return Object.assign({}, action, payload.data);
                        }
                        return action;
                    });
                });
        },
        [CashgameStoreMutations.DataLoaded](state, data: DetailedCashgame) {
            state._isRunning = data.isRunning;
            state._id = data.id;
            state._slug = data.bunch.id;
            state._locationId = data.location.id;
            state._locationName = data.location.name;
            state._updatedTime = dayjs(data.updatedTime).toDate();
            state._players = data.players;
        }
    }
} as StoreOptions<CashgameStoreState>;

function setupRefresh(context: ActionContext<CashgameStoreState, CashgameStoreState>, refreshTimeout: number) {
    if (context.state._isRunning) {
        window.setTimeout(() => {
                refresh(context);
            }, refreshTimeout);
    }
}

async function refresh(context: ActionContext<CashgameStoreState, CashgameStoreState>) {
    try{
        const response = await api.getCashgame(context.state._id);
        if (response.status === 200) {
            setPlayers(context, response.data);
        } else {
            console.log('refresh error');
        }
    } catch {
        console.log('refresh error');
    }
}

function setPlayers(context: ActionContext<CashgameStoreState, CashgameStoreState>, data: DetailedCashgameResponse) {
    context.commit(CashgameStoreMutations.SetPlayers, data.players);
    context.commit(CashgameStoreMutations.SetIsRunning, data.isRunning);
    setupRefresh(context, longRefresh);
}

function addCheckpoint(type: DetailedCashgameResponseActionType, player: DetailedCashgameResponsePlayer, stack: number, added: number | null) {
    const action = { id: null, type, time: dayjs().utc().toDate(), stack, added };
    player.actions.push(action);
}

function createPlayer(playerId: string, playerName: string | null, playerColor: string | null): DetailedCashgameResponsePlayer {
    return {
        id: playerId,
        name: playerName || '',
        color: playerColor || '#9e9e9e',
        startTime: null,
        updatedTime: null,
        buyin: null,
        stack: null,
        actions: []
    };
}

function mapCashgame(response: DetailedCashgameResponse): DetailedCashgame {
    return {
        isRunning: response.isRunning,
        id: response.id,
        bunch: {
            id: response.bunch.id,
            timezone: response.bunch.timezone,
            currencyFormat: response.bunch.currencyFormat,
            currencySymbol: response.bunch.currencySymbol,
            currencyLayout: response.bunch.currencyLayout,
            thousandSeparator: response.bunch.thousandSeparator,
            culture: response.bunch.culture,
            role: response.bunch.role
        },
        location: {
            id: response.location.id,
            name: response.location.name
        },
        startTime: dayjs(response.startTime).toDate(),
        updatedTime: dayjs(response.updatedTime).toDate(),
        players: response.players.map((o) => mapCashgamePlayer(o)),
        event: response.event ? {
            id: response.event.id,
            name: response.event.name
        } : null,
    }
}

function mapCashgamePlayer(response: DetailedCashgameResponsePlayer): DetailedCashgamePlayer{
    return {
        id: response.id,
        name: response.name,
        color: response.color,
        startTime: response.startTime ? dayjs(response.startTime).toDate() : null,
        updatedTime: response.updatedTime ? dayjs(response.updatedTime).toDate() : null,
        buyin: response.buyin,
        stack: response.stack,
        actions: response.actions.map((o) => mapCashgameAction(o))
    }
}

function mapCashgameAction(response: DetailedCashgameResponseAction): DetailedCashgameAction{
    return {
        id: response.id,
        type: response.type,
        time: dayjs(response.time).toDate(),
        stack: response.stack,
        added: response.added
    }
}