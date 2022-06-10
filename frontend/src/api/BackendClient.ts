import { IPkoExcelReportingClient } from "./IPkoExcelReportingClient";
import { MockPkoExcelReportingClient } from "./MockPkoExcelReportingClient";
import { PkoExcelReportingClient } from "./PkoExcelReportingClient";

export const BackendClient: IPkoExcelReportingClient = new PkoExcelReportingClient("http://nsatin.ru/api/excelReporting");
