import { Result } from "./Result";
import { ParseFileResponse } from "./ParseFileResponse";
import { CalculateNextRequest } from "./CalculateNextRequest";
import { CalculateNextResponse } from "./CalculateNextResponse";

export interface IPkoExcelReportingClient {
    parseNext: (content: string) => Promise<Result<ParseFileResponse>>;
    calculateNext: (request: CalculateNextRequest) => Promise<Blob>
}

