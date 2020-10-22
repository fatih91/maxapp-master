import {Checklist} from "./checklist.model";
import {Prognose} from "./prognose.model";
import {Therapy} from "./therapy.model";
import {Differentialdiagnose} from "./differentialdiagnose.model";
import {DiagnoseSymptom} from "./diagnoseSymptom.model";
import {Tag} from "./tag.model";
import {KeyValuePair} from "./keyValuePair.model";
import {Image} from "./image.model";

export interface Diagnose{
    diagnoseId?: number;
    technicalTerm: string;
    synonyms?: KeyValuePair[];
    icds?: KeyValuePair[];
    definition?: string;
    inheritance?: string;
    reason?: string;
    diagnostics?: KeyValuePair[];
    ageTime?: string;
    season?: string;
    prevalence?: string;
    checklists?: Checklist[];
    prognoses?: KeyValuePair[];
    therapies?: KeyValuePair[];
    tags?: Tag[];
    image?: Image;
    symptoms?: DiagnoseSymptom[];
    differentialdiagnoses?: Differentialdiagnose[];
}
