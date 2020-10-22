import {Therapy} from "./therapy.model";
import {Differentialdiagnose} from "./differentialdiagnose.model";
import {Checklist} from "./checklist.model";
import {Prognose} from "./prognose.model";
import {DiagnoseSymptom} from "./diagnoseSymptom.model";
import {Tag} from "./tag.model";
import {KeyValuePair} from "./keyValuePair.model";

export interface SaveDiagnose{

    diagnoseId?: number;
    technicalTerm?: string;
    synonyms?: KeyValuePair[];
    icds?: KeyValuePair[];
    definition?: string;
    inheritance?: string;
    reason?: string;
    diagnostics?: KeyValuePair[];
    ageTime?: string;
    season?: string;
    prevalence?: string;
    fileName?: string;
    tags?: number[];
    checklists?: Checklist[];
    prognoses?: KeyValuePair[];
    therapies?: KeyValuePair[];
    symptoms?: number[];
    differentialdiagnoses?: number[];
}
