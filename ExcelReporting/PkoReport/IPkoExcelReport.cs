using ExcelReporting.Client;

namespace ExcelReporting.PkoReport;

public interface IPkoExcelReport
{
    IPkoExcelReport UpdateComplicationDate(Date date);
    
    IPkoExcelReport UpdateDocumentNumber(int documentNumber);
    
    IPkoExcelReport UpdateDebit(int roubles, int kopecks);
    
    IPkoExcelReport UpdateZCause(int number, Date date);

    IPkoExcelReport UpdateAcceptedBy(string name);
    
    void Save();
}