import { Nullable } from "@skbkontur/react-ui/cjs/typings/utility-types";

export interface Date {
    year: number;
    month: number;
    day: number;
}

export interface Currency {
    kopecks: number;
    roubles: number;
}

export class CurrencyHelper {
    public static convertToNumber(value: Nullable<Currency>): number | undefined {
        if (value == null) {
            return undefined;
        }

        return parseFloat(`${value.roubles}.${value.kopecks}`);
    }

    public static parseFromNumber(value: Nullable<number>): Currency | undefined {
        if (value == null) {
            return undefined;
        }
        
        return {
            roubles: Math.trunc(value),
            kopecks: this.parseDecimals(value),
        };
    }

    private static parseDecimals(value: number): number {
        const valStr = value.toString();
        const valTruncLength = String(Math.trunc(value)).length;

        const dec = valStr.length != valTruncLength ? valStr.substring(valTruncLength + 1, valTruncLength + 3) : "0";

        return parseInt(dec);
    }
}
