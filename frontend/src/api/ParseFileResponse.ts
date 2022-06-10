import { Currency, Date } from "./Date";

export interface ParseFileResponse {
    shopAddress: string;
    lastComplicationDate: Date;
    lastDocumentNumber: number;
    lastZCauseNumber: number;
    acceptedByPersons: string[];
    lastAcceptedByPerson: string;
}