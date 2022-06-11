import { Result } from "./Result";
import { ParseFileResponse } from "./ParseFileResponse";
import { ParseFileRequest } from "./ParseFileRequest";
import { IPkoExcelReportingClient } from "./IPkoExcelReportingClient";
import { CalculateNextRequest } from "./CalculateNextRequest";
import { CalculateNextResponse } from "./CalculateNextResponse";

export class PkoExcelReportingClient implements IPkoExcelReportingClient {
    private readonly baseUrl: string;

    constructor(baseUrl: string) {
        this.baseUrl = `${baseUrl}/pko`;
    }

    async parseNext(base64Content: string): Promise<Result<ParseFileResponse>> {
        const response = await this.post<ParseFileRequest, ParseFileResponse>("parseNext", {
            excelContentBase64: base64Content,
        });

        return response;
    }

    async calculateNext(request: CalculateNextRequest): Promise<Blob> {
        const response = await fetch(`${this.baseUrl}/calculateNext`, {
            method: "POST",
            body: JSON.stringify(request),
            headers: {
                "Content-Type": "application/json",
            },
        });

        return await response.blob();
    };

    private async post<TRequest, TResponse>(url: string, body: TRequest): Promise<Result<TResponse>> {
        const response = await fetch(`${this.baseUrl}/${url}`, {
            method: "POST",
            body: JSON.stringify(body),
            headers: {
                "Content-Type": "application/json",
            },
        });

        if (response.ok) {
            const result = await response.json();
            return Result.Ok<TResponse>(result);
        }

        console.error(`${response.status};${response.statusText}`);
        return Result.Error<TResponse>(response.status, response.statusText);
    }
}
