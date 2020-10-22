import {Diagnose} from "./diagnose.model";
import {Symptom} from "./symptom.model";
import {KeyValuePair} from "./keyValuePair.model";

export interface DiagnoseSymptom{

    id : number;
    technicalTerm: string;
    synonym?: string;
    definition?: string;

}
