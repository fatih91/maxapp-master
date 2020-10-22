import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Filter} from "../models/filter.model";
import {Observable} from "rxjs/Observable";
import {Category} from "../models/category.model";
import {SaveCategory} from "../models/saveCategory.model";
import {Subcategory} from "../models/subcategory.model";
import {Parentcategory} from "../models/parentcategory.model";
import {MapCategory} from "../models/mapCategory.model";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'Bearer ' + localStorage.getItem("auth_token")
  })
};

@Injectable()
export class CategoryService {
    private readonly categoryEndpoint = "api/categories";
    baseUrl: string;

    constructor(private http: HttpClient){}

    getCategories(): Observable<MapCategory[]>{
      return this.http.get<MapCategory[]>(this.categoryEndpoint);
    }

    getCategory(id: number): Observable<Category> {
        return this.http.get<Category>(this.categoryEndpoint + '/' + id);
    }

    createCategory(category: SaveCategory): Observable<Category> {
        return this.http.post<Category>(this.categoryEndpoint, category);
    }

    getParentCategory(id: number){
        return this.http.get<Subcategory>(this.categoryEndpoint + '/' + id) //return type korrekt?
    }

    getParentCategories(id: number): Observable<Parentcategory[]>{
        return this.http.get<Parentcategory[]>(this.categoryEndpoint + '/parentcategories/' + id) //return type korrekt?
    }

    getSubcategories(id: number): Observable<Category>{
        return this.http.get<Category>(this.categoryEndpoint + '/' + id);
    }

    deleteCategory(id: number): Observable<{}> {
        const url = `${this.categoryEndpoint}/${id}`;
        return this.http.delete(url,  httpOptions);
    }

    updateCategory(category : Category): Observable<Category> {
        return this.http.put<Category>(this.categoryEndpoint, category);
    }

}
