import { ArchiveCashgameResponse } from '@/response/ArchiveCashgameResponse';
import timeFunctions from '@/time-functions';
import dayjs from 'dayjs';
import { ArchiveCashgameLocation } from './ArchiveCashgameLocation';
import { ArchiveCashgamePlayer } from './ArchiveCashgamePlayer';

export class ArchiveCashgame {
    id: string;
    startTime: Date;
    updatedTime: Date;
    location: ArchiveCashgameLocation;
    players: ArchiveCashgamePlayer[];
    turnover: number;
    averageBuyin: number;
    duration: number;

    constructor(id: string, startTime: Date, updatedTime: Date, location: ArchiveCashgameLocation, players: ArchiveCashgamePlayer[]) {
        this.id = id;
        this.startTime = startTime;
        this.updatedTime = updatedTime;
        this.location = location;
        this.players = players.sort((a, b) => a.winnings - b.winnings).reverse();
        this.turnover = this.getTurnover();
        this.averageBuyin = this.getAverageBuyin();
        this.duration = timeFunctions.diffInMinutes(startTime, updatedTime)
    }

    public get date(){
        return this.startTime;
    }

    public get playerCount(){
        return this.players.length;
    }

    public isBestPlayer(playerId: string){
        if(this.players.length === 0)
            return false;

        return this.players[0].id === playerId;
    }

    private getAverageBuyin() {
        const sum = this.getTurnover();
        const playerCount = this.players.length;
        return Math.round(sum / playerCount);
    }

    private getTurnover() {
        let sum = 0;
        for (const p of this.players) {
            sum += p.buyin;
        }
        return sum;
    }

    public static fromResponse(response: ArchiveCashgameResponse){
        return new ArchiveCashgame(
            response.id,
            dayjs(response.startTime).toDate(),
            dayjs(response.updatedTime).toDate(),
            ArchiveCashgameLocation.fromResponse(response.location),
            response.players.map(o => ArchiveCashgamePlayer.fromResponse(o))
        );
    }
}