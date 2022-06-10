export class Result<T> {
    value?: T;
    code: number;
    errorMessage?: string;

    constructor(code: number, value?: T, errorMessage?: string) {
        this.code = code;
        this.value = value;
        this.errorMessage = errorMessage;
    }

    static Ok<T>(value?: T): Result<T> {
        return new Result<T>(200, value);
    }

    static Error<T>(code: number, errorMessage?: string): Result<T> {
        return new Result<T>(code, undefined, errorMessage);
    }
}
