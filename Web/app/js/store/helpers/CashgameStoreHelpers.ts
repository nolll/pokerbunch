﻿import { DetailedCashgameResponsePlayer } from '@/response/DetailedCashgameResponsePlayer';

export enum CashgameStoreGetters {
    CashgameId = 'cashgame_id',
    IsRunning = 'cashgame_isRunning',
    LocationId = 'cashgame_locationId',
    LocationName = 'cashgame_locationName',
    ReportFormVisible = 'cashgame_reportFormVisible',
    BuyinFormVisible = 'cashgame_buyinFormVisible',
    CashoutFormVisible = 'cashgame_cashoutFormVisible',
    Players = 'cashgame_players',
    HasPlayers = 'cashgame_hasPlayers',
    PlayerId = 'cashgame_playerId',
    SortedPlayers = 'cashgame_sortedPlayers',
    GetPlayer = 'cashgame_getPlayer',
    IsInGame = 'cashgame_isInGame',
    CanReport = 'cashgame_canReport',
    CanBuyin = 'cashgame_canBuyin',
    CanCashout = 'cashgame_canCashout',
    HasCashedOut = 'cashgame_hasCashedOut',
    Player = 'cashgame_player',
    TotalStacks = 'cashgame_totalStacks',
    TotalBuyin = 'cashgame_totalBuyin',
    StartTime = 'cashgame_startTime',
    UpdatedTime = 'cashgame_updatedTime',
    CashgameReady = 'cashgame_cashgameReady'
}

export enum CashgameStoreActions {
    LoadCashgame = 'cashgame_loadCashgame',
    SelectPlayer = 'cashgame_selectPlayer',
    Refresh = 'cashgame_refresh',
    Report = 'cashgame_report',
    Buyin = 'cashgame_buyin',
    FirstBuyin = 'cashgame_firstBuyin',
    Cashout = 'cashgame_cashout',
    ShowReportForm = 'cashgame_showReportForm',
    ShowBuyinForm = 'cashgame_showBuyinForm',
    ShowCashoutForm = 'cashgame_showCashoutForm',
    HideForms = 'cashgame_hideForms',
    DeleteAction = 'cashgame_deleteAction',
    SaveAction = 'cashgame_saveAction'
}

export enum CashgameStoreMutations {
    SetPlayerId = 'cashgame_setPlayerId',
    ResetPlayerId = 'cashgame_resetPlayerId',
    SetPlayers = 'cashgame_setPlayers',
    SetIsRunning = 'cashgame_setIsRunning',
    Report = 'cashgame_report',
    Cashout = 'cashgame_cashout',
    Buyin = 'cashgame_buyin',
    AddPlayer = 'cashgame_addPlayer',
    ShowReportForm = 'cashgame_showReportForm',
    ShowBuyinForm = 'cashgame_showBuyinForm',
    ShowCashoutForm = 'cashgame_showCashoutForm',
    HideForms = 'cashgame_hideForms',
    LoadingComplete = 'cashgame_loadingComplete',
    RemoveAction = 'cashgame_removeAction',
    UpdateAction = 'cashgame_updateAction',
    DataLoaded = 'cashgame_dataLoaded'
}

export interface CashgameStoreState {
    _slug: string;
    _id: string;
    _isRunning: boolean;
    _playerId: string | null;
    _locationId: string;
    _locationName: string;
    _players: DetailedCashgameResponsePlayer[];
    _currentStack: number;
    _updatedTime: Date | null;
    _reportFormVisible: boolean;
    _buyinFormVisible: boolean;
    _cashoutFormVisible: boolean;
    _cashgameReady: boolean;
}

export interface BuyinParams{
    amount: number;
    stack: number;
}

export interface FirstBuyinParams{
    amount: number;
    stack: number;
    name: string;
    color: string;
}

export interface DeleteActionParams{
    id: string;
}