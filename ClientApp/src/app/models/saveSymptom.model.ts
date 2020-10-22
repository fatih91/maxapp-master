import {KeyValuePair} from "./keyValuePair.model";

export interface SaveSymptom {
    symptomId?: number;
    technicalTerm?: string;
    synonyms?: KeyValuePair[];
    definition?: string;
    categoryId?: number;
    tags?: number[];
    diagnoses?: number[];
}
