import {Subcategory} from "./subcategory.model";

export interface Category {
    categoryId?: number;
    name?: string;
    subcategories?: Subcategory[];
    symptoms?: CategorySymptom[];
}
