export class FileHelper {
    static async parseFileToBase64Async(file: File): Promise<string> {
        return new Promise((resolve, _) => {
            const reader = new FileReader();
            reader.onloadend = () => resolve((reader.result as string).split(',')[1]);
            reader.readAsDataURL(file);
        });
    }

    static async validateFileName(fileName: string): Promise<string | null> {
        if (fileName.endsWith("xls") || fileName.endsWith("xlsx")) {
            return Promise.resolve(null);
        }

        return Promise.resolve("Допустимые расширения файла xls и xlsx");
    }
}