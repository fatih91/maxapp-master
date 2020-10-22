import {Image} from "./image.model";
import {DiagnoseSymptom} from "./diagnoseSymptom.model";
import {Category} from "./category.model";
import {Tag} from "./tag.model";
import {SymptomDiagnose} from "./symptomDiagnose.model";
import {SymptomCategory} from "./symptom-category.model";
import {KeyValuePair} from "./keyValuePair.model";

export interface Symptom {
        symptomId?: number;
        technicalTerm?: string;
        synonyms?: KeyValuePair[];
        definition?: string;
        tags?: Tag[];
        diagnoses?: SymptomDiagnose[];
        images?: Image[];
        category?: SymptomCategory;

}
