import {
    Button,
    ComboBox,
    CurrencyInput,
    DateInput,
    FileUploader,
    FileUploaderAttachedFile,
    Gapped,
    Input,
    MenuSeparator,
    Toast,
} from "@skbkontur/react-ui";
import React from "react";
import { Col, Container, Row } from "react-grid-system";
import { BackendClient } from "../api/BackendClient";
import { Currency, CurrencyHelper, Date } from "../api/Date";
import { DateHelper } from "../api/DateHelper";
import { FileHelper } from "./FileParser";

export interface PkoPageProps {}

export interface PkoPageState {
    fileUploaded?: boolean;
    isNextLoaded?: boolean;
    shopAddress?: string;
    documentNumber?: number;
    fileBase64Content?: string;
    complicationDate?: Date;
    zCauseNumber?: number;
    acceptedByPersons: string[];
    lastAcceptedByPerson?: string;
    debitAmount?: Currency;
}

export class PkoPage extends React.Component<PkoPageProps, PkoPageState> {
    private readonly leftPaddingMd = 0.1;
    private readonly firstColumnMd = 2;

    state: PkoPageState = {
        acceptedByPersons: [],
    };

    private uploadFileAsync = async (attachedFile: FileUploaderAttachedFile): Promise<void> => {
        const base64Content = await FileHelper.parseFileToBase64Async(attachedFile.originalFile);
        const response = await BackendClient.parseNext(base64Content);
        if (response.code == 200) {
            this.setState((prevState) => ({
                ...prevState,
                fileUploaded: true,
                shopAddress: response.value?.shopAddress,
                documentNumber: response.value?.lastDocumentNumber,
                fileBase64Content: base64Content,
                complicationDate: response.value?.lastComplicationDate,
                zCauseNumber: response.value?.lastZCauseNumber,
                lastAcceptedByPerson: response.value?.lastAcceptedByPerson,
                acceptedByPersons: response.value?.acceptedByPersons ?? [],
            }));
            Toast.push("Новые данные для следующего листка расчитаны.");
        } else {
            Toast.push("Произошла ошибка. Попробуйте ещё раз.");
        }
    };

    private renderFileUploade(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={3}>
                        <span>Укажите путь до файла ПКО (.xls/.xlsx)</span>
                    </Col>
                    <Col>
                        <FileUploader
                            request={this.uploadFileAsync}
                            validateBeforeUpload={async (f) => {
                                const fileName = f.originalFile.name;
                                return await FileHelper.validateFileName(fileName);
                            }}
                        />
                    </Col>
                </Row>
            </>
        );
    }

    private renderShopAddress(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={this.firstColumnMd}>Адрес магазина:</Col>
                    <Col>{this.state?.shopAddress ?? "Не определен"}</Col>
                </Row>
            </>
        );
    }

    private renderDocumentNumber(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={this.firstColumnMd}>Номер документа:</Col>
                    <Col>
                        <Input
                            width={"150px"}
                            disabled={!this.state?.fileUploaded}
                            mask={"99999"}
                            onValueChange={(value) =>
                                this.setState((prevState) => ({
                                    ...prevState,
                                    documentNumber: parseInt(value ?? ""),
                                }))
                            }
                            value={this.state?.documentNumber?.toString()}
                        />
                    </Col>
                </Row>
            </>
        );
    }

    private renderComplicationDate(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={this.firstColumnMd}>Дата составления:</Col>
                    <Col>
                        <DateInput
                            width={"150px"}
                            disabled={!this.state?.fileUploaded}
                            onValueChange={(value) =>
                                this.setState((prevState) => ({
                                    ...prevState,
                                    complicationDate: DateHelper.parseFromString(value),
                                }))
                            }
                            value={DateHelper.convertToString(this.state?.complicationDate)}
                        />
                    </Col>
                </Row>
            </>
        );
    }

    private renderZCauseNumber(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={this.firstColumnMd}>Номер Z отчета:</Col>
                    <Col>
                        <Input
                            width={"150px"}
                            disabled={!this.state?.fileUploaded}
                            mask={"99999"}
                            onValueChange={(value) =>
                                this.setState((prevState) => ({
                                    ...prevState,
                                    zCauseNumber: parseInt(value ?? ""),
                                }))
                            }
                            value={this.state?.zCauseNumber?.toString()}
                        />
                    </Col>
                </Row>
            </>
        );
    }

    private renderAcceptedByPerson(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={this.firstColumnMd}>Принят от:</Col>
                    <Col>
                        <ComboBox
                            width={"150px"}
                            disabled={!this.state?.fileUploaded}
                            getItems={(x) =>
                                Promise.resolve(
                                    this.state?.acceptedByPersons
                                        ?.map((o) => ({
                                            value: o,
                                            label: o,
                                        }))
                                        .filter((o) => o.value.includes(x))
                                )
                            }
                            onValueChange={(value) =>
                                this.setState((prevState) => ({
                                    ...prevState,
                                    lastAcceptedByPerson: value.value,
                                }))
                            }
                            value={{ value: this.state?.lastAcceptedByPerson, label: this.state?.lastAcceptedByPerson }}
                        ></ComboBox>
                    </Col>
                </Row>
            </>
        );
    }

    private renderDebitAmount(): JSX.Element {
        return (
            <>
                <Row>
                    <Col md={this.leftPaddingMd}></Col>
                    <Col md={this.firstColumnMd}>Дебет:</Col>
                    <Col>
                        <CurrencyInput
                            align="left"
                            disabled={!this.state.fileUploaded}
                            onValueChange={(value) =>
                                this.setState((prevState) => ({
                                    ...prevState,
                                    debitAmount: CurrencyHelper.parseFromNumber(value),
                                }))
                            }
                            value={CurrencyHelper.convertToNumber(this.state.debitAmount)}
                            width={"150px"}
                        />
                    </Col>
                </Row>
            </>
        );
    }

    private async calculateNext(): Promise<void> {
        const state = this.state;
        state.isNextLoaded = true;
        this.setState(state);
        const response = await BackendClient.calculateNext({
            debit: this.state.debitAmount as Currency,

            excelContentBase64: this.state.fileBase64Content as string,
            complicationDate: this.state.complicationDate as Date,
            documentNumber: this.state.documentNumber as number,
            zCauseNumber: this.state.zCauseNumber as number,
            acceptedByPerson: this.state.lastAcceptedByPerson as string,
        });

        state.isNextLoaded = false;
        this.setState(state);

        /*const byteString = response.value;
        const ab = new ArrayBuffer(byteString.length);
        const ia = new Uint8Array(ab);

        for (var i = 0; i < byteString.length; i++) {
            ia[i] = byteString.charCodeAt(i);
        }*/
        const date = this.state.complicationDate;
        const blob = response;
        const objUrl = URL.createObjectURL(blob);
        const anchor = document.createElement("a");
        anchor.href = objUrl;
        const dateStr = DateHelper.convertToString(date);
        anchor.download = `ПКО_${dateStr}.xlsx`;
        document.body.appendChild(anchor);
        anchor.click();
        document.body.removeChild(anchor);

        URL.revokeObjectURL(objUrl);
    }

    render(): JSX.Element {
        return (
            <>
                <Container fluid>
                    <Gapped vertical gap={10}>
                        <div></div>
                        {this.renderFileUploade()}
                        <MenuSeparator />
                        {this.renderShopAddress()}
                        {this.renderDocumentNumber()}
                        {this.renderComplicationDate()}
                        {this.renderZCauseNumber()}
                        {this.renderAcceptedByPerson()}
                        {this.renderDebitAmount()}
                        <MenuSeparator />
                        <Row>
                            <Col md={this.leftPaddingMd}></Col>
                            <Col md={this.firstColumnMd}>
                                <Button
                                    disabled={!this.state.fileUploaded}
                                    loading={this.state.isNextLoaded}
                                    onClick={async () => await this.calculateNext()}
                                >
                                    Сформировать новую страницу
                                </Button>
                            </Col>
                        </Row>
                    </Gapped>
                </Container>
            </>
        );
    }
}
