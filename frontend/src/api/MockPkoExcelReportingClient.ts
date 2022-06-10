import { Result } from "./Result";
import { ParseFileResponse } from "./ParseFileResponse";
import { IPkoExcelReportingClient } from "./IPkoExcelReportingClient";
import { CalculateNextRequest } from "./CalculateNextRequest";
import { CalculateNextResponse } from "./CalculateNextResponse";

export class MockPkoExcelReportingClient implements IPkoExcelReportingClient {
    parseNext(content: string): Promise<Result<ParseFileResponse>> {
        return Promise.resolve({
            value: {
                shopAddress: "Каменск-Уральский",
                lastComplicationDate: {
                    year: 2020,
                    month: 8,
                    day: 8,
                },
                lastDocumentNumber: 1337,
                lastZCauseNumber: 213,
                acceptedByPersons: ["Satin", "Ivanov"],
                lastAcceptedByPerson: "Satin",
            },
            code: 200,
        });
    }

    calculateNext(request: CalculateNextRequest): Promise<Blob> {
        throw new Error();
    };
}
