import { Currency, Date } from "./Date";


export interface CalculateNextRequest {
    excelContentBase64: string;
    complicationDate: Date;
    documentNumber: number;
    zCauseNumber: number;
    acceptedByPerson: string;
    debit: Currency;
}
