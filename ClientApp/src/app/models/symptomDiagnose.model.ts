import {KeyValuePair} from "./keyValuePair.model";

export interface SymptomDiagnose{

    diagnoseId?: number;
    icds?: KeyValuePair[];
    technicalTerm?: string;
    synonyms?: KeyValuePair[];
    definition?: string;
    prevalence?: string;
}
