import { ArchiveCashgameResultResponse } from '@/response/ArchiveCashgameResultResponse';
import timeFunctions from '@/time-functions';
import dayjs from 'dayjs';

export class ArchiveCashgamePlayer {
    id: string;
    name: string;
    startTime: Date;
    updatedTime: Date;
    buyin: number;
    stack: number;
    winnings: number;
    timePlayed: number;

    constructor(id: string, name: string, startTime: Date, updatedTime: Date, buyin: number, stack: number) {
        this.id = id;
        this.name = name;
        this.startTime = startTime;
        this.updatedTime = updatedTime;
        this.buyin = buyin;
        this.stack = stack;
        this.winnings = stack - buyin;
        this.timePlayed = timeFunctions.diffInMinutes(this.startTime, this.updatedTime);
    }

    public static fromResponse(response: ArchiveCashgameResultResponse){
        return new ArchiveCashgamePlayer(
            response.id,
            response.name,
            dayjs(response.startTime).toDate(),
            dayjs(response.updatedTime).toDate(),
            response.buyin,
            response.stack
        );
    }
}