import { Date } from "./Date";

export class DateHelper {
    public static parseFromString(value: string): Date | undefined {
        if (value == null) {
            return undefined;
        }
        const splittedValues = value.split(".");
        if (splittedValues.length != 3) {
            return undefined;
        }

        return {
            day: parseInt(splittedValues[0]),
            month: parseInt(splittedValues[1]),
            year: parseInt(splittedValues[2]),
        };
    }

    public static convertToString(value: Date | undefined): string {
        if (value == null) {
            return "";
        }

        const day = value.day.toString().padStart(2, "0");
        const month = value.month.toString().padStart(2, "0");
        const year = value.year.toString();

        return `${day}.${month}.${year}`;
    }
}
