import { DetailedCashgameResponse } from '@/response/DetailedCashgameResponse';
import dayjs from 'dayjs';
import { DetailedCashgameBunch } from './DetailedCashgameBunch';
import { DetailedCashgameEvent } from './DetailedCashgameEvent';
import { DetailedCashgameLocation } from './DetailedCashgameLocation';
import { DetailedCashgamePlayer } from './DetailedCashgamePlayer';

export class DetailedCashgame {
    id: string;
    bunch: DetailedCashgameBunch;
    location: DetailedCashgameLocation;
    startTime: Date;
    updatedTime: Date;
    players: DetailedCashgamePlayer[];
    event: DetailedCashgameEvent | null;

    constructor(response: DetailedCashgameResponse) {
        this.id = response.id;
        this.bunch = new DetailedCashgameBunch(response.bunch);
        this.location = DetailedCashgameLocation.fromResponse(response.location);
        this.startTime = dayjs(response.startTime).toDate();
        this.updatedTime = dayjs(response.updatedTime).toDate();
        this.players = response.players.map((o) => DetailedCashgamePlayer.fromResponse(o)),
        this.event = !!response.event
            ? DetailedCashgameEvent.fromResponse(response.event)
            : null;
    }

    public get isRunning(){
        if(this.players.length === 0)
            return true;

        for(const player of this.players){
            if (!player.hasCashedOut())
                return true;
        }
        return false;
    }

    public deleteAction(id: string){
        for(const p of this.players){
            const index = p.actions.map(item => item.id).indexOf(id);
            if (index !== -1) {
                p.actions.splice(index, 1);
                if(p && p.actions.length === 0){
                    this.players = this.players.filter(o => o.id !== p.id);
                }
                break;
            }
        }
    }

    public updateAction(id: string, data: any){
        this.players.forEach((player) => {
            player.actions = player.actions.map(action => {
                if (action.id === data.id)
                    return Object.assign({}, action, data);
                return action;
            });
        });
    }

    public update(location: DetailedCashgameLocation, event: DetailedCashgameEvent | null){
        this.location = location;
        this.event = event;
    }

    public report(playerId: string, stack: number){
        const player = this.getPlayer(playerId);
        if(!player)
            return;
        player.addReport(stack);
    }

    public buyin(playerId: string, added: number, stack: number){
        const player = this.getPlayer(playerId);
        if(!player)
            return;
        player.addBuyin(added, stack + added);
    }

    public cashout(playerId: string, stack: number){
        const player = this.getPlayer(playerId);
        if(!player)
            return;
        player.addCashout(stack);
    }

    public addPlayer(id: string, name: string, color: string){
        this.players.push(DetailedCashgamePlayer.new(id, name, color));
    }

    private getPlayer(id: string){
        if (!id)
            return null;
        return this.players.find(p => p.id.toString() === id.toString()) || null;
    }
}
