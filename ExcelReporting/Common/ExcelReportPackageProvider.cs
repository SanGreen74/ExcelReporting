using OfficeOpenXml;

namespace ExcelReporting.Common;

public static class ExcelReportPackageProvider
{
    public static ExcelPackage Get(byte[] bytes)
    {
        return new ExcelPackage(new MemoryStream(bytes));
    }
}