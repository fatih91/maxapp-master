import {Subcategory} from "./subcategory.model";

export interface MapCategory {
    name?: string;
    categoryId?: number;
    subcategories?: Subcategory[];
}
