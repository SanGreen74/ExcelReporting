import { IPkoExcelReportingClient } from "./IPkoExcelReportingClient";
import { MockPkoExcelReportingClient } from "./MockPkoExcelReportingClient";
import { PkoExcelReportingClient } from "./PkoExcelReportingClient";

const url = process.env.NODE_ENV == "production" 
? "http://nsatin.ru"
: "http://localhost:5003";

export const BackendClient: IPkoExcelReportingClient = new PkoExcelReportingClient(`${url}/api/excelReporting`);