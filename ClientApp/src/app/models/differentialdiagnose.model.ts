import {Diagnose} from "./diagnose.model";
import {KeyValuePair} from "./keyValuePair.model";

export interface Differentialdiagnose{
    diagnoseId?: number;
    technicalTerm?: string;
    icds?: KeyValuePair[];
    synonyms?: KeyValuePair[];
    prevalence?: string;
}
