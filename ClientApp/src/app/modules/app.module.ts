import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AppComponent} from "../components/app/app.component";
import {NavMenuComponent} from "../components/nav-menu/navmenu.component";
import {HomeLayoutComponent} from "../components/layouts/home-layout.component";
import {HomeComponent} from "../components/home/home.component";
import {DiagnoseViewComponent} from "../components/diagnoses/diagnose-view/diagnose-view.component";
import {SymptomViewComponent} from "../components/symptoms/symptom-view/symptom-view.component";
import {DiagnoseFilterListComponent} from "../components/diagnoses/diagnose-filter-list/diagnose-filter-list.component";
import {SymptomFilterListComponent} from "../components/symptoms/symptom-filter-list/symptom-filter-list.component";
import {SymptomTableComponent} from "../components/diagnoses/symptom-table/symptom-table.component";
import {DifferentialDiagnoseTableComponent} from "../components/diagnoses/differentialdiagnose-table/differentialdiagnose-table.component";
import {DiagnoseBasicsComponent} from "../components/diagnoses/diagnose-basics/diagnose-basics.component";
import {ArrayToStringPipe} from "../pipes/ArrayToStringPipe";
import {FirstElementPipe} from "../pipes/FirstElementPipe";
import {FirstElementOfImagesPipe} from "../pipes/FirstElementOfImagesPipe";
import {ImageSliderComponent} from "../components/common/image-slider/image-slider.component";
import {DiagnoseCreateComponent} from "../components/diagnoses/diagnose-create/diagnose-create.component";
import {ShowErrorComponent} from "../components/common/show-error/show-error.component";
import {DiagnoseLayoutComponent} from "../components/layouts/diagnose-layout.component";
import {MyDiagnosesListComponent} from "../components/diagnoses/my-diagnoses-list/my-diagnoses-list.component";
import {TagComponent} from "../components/common/tag/tag.component";
import {Int2GenderPipe} from "../pipes/Int2Gender";
import {SymptomCreateComponent} from "../components/symptoms/symptom-create/symptom-create.component";
import {IcdValidator} from "../validators/icd-validator";
import {UniqueDiagnoseTechnicalTermValidator} from "../validators/UniqueDiagnoseTechnicalTermValidator";
import {UniqueSymptomTechnicalTermValidator} from "../validators/UniqueSymptomTechnicalTermValidator";
import {ImageDialogComponent} from "../components/common/image-dialog/image-dialog.component";
import {SymptomCategoriesCreateComponent} from "../components/categories/symptom-categories-create/symptom-categories-create.component";
import {MyCreatedSymptomsComponent} from "../components/symptoms/my-created-symptoms/my-created-symptoms.component";
import {SymptomLayoutComponent} from "../components/layouts/symptom-layout.component";
import {PopupImageComponent} from "../components/common/popup-image/popup-image.component";
import {LoginComponent} from "../components/login/login.component";
import {CategoriesViewComponent} from "../components/categories/categories-view/categories-view.component";
import {SymptomCategoryViewComponent} from "../components/categories/symptom-category-view/symptom-category-view.component";
import {FilterPipe} from "../pipes/FilterPipe";
import {TagCreateComponent} from "../components/common/tag-create/tag-create.component";
import {DiagnoseTableComponent} from "../components/common/diagnose-table/diagnose-table.component";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {CommonModule} from "@angular/common";
import {MaterialModule} from "./material.module";
import {TagService} from "../services/tag.service";
import {DragScrollModule} from "ngx-drag-scroll/lib";
import {appRouting} from "../routing/app.routing";
import {MatSelectModule} from "@angular/material";
import {DiagnoseService} from "../services/diagnose.service";
import {SymptomService} from "../services/symptom.service";
import {CategoryService} from "../services/category.service";
import {ImageService} from "../services/image.service";
import {AccountService} from "../services/account.service";
import {UserService} from "../services/user.service";
import {AuthGuard} from "../routing/auth.guard";
import {AlreadyLoggedGuard} from "../routing/already-logged.guard";
import {AuthInterceptor} from "../services/AuthInterceptor";



@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeLayoutComponent,
    HomeComponent,
    DiagnoseViewComponent,
    SymptomViewComponent,
    DiagnoseFilterListComponent,
    SymptomFilterListComponent,
    SymptomTableComponent,
    DifferentialDiagnoseTableComponent,
    DiagnoseBasicsComponent,
    ArrayToStringPipe,
    FirstElementPipe,
    FirstElementOfImagesPipe,
    ImageSliderComponent,
    DiagnoseCreateComponent,
    Int2GenderPipe,
    DiagnoseLayoutComponent,
    MyDiagnosesListComponent,
    TagComponent,
    ShowErrorComponent,
    SymptomCreateComponent,
    ImageDialogComponent,
    UniqueDiagnoseTechnicalTermValidator,
    UniqueSymptomTechnicalTermValidator,
    IcdValidator,
    SymptomCategoriesCreateComponent,
    MyCreatedSymptomsComponent,
    SymptomLayoutComponent,
    PopupImageComponent,
    LoginComponent,
    CategoriesViewComponent,
    SymptomCategoryViewComponent,
    FilterPipe,
    TagCreateComponent,
    DiagnoseTableComponent
  ],
  entryComponents:[
    ImageDialogComponent,
    PopupImageComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    CommonModule,
    MaterialModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    MatSelectModule,
    DragScrollModule, // https://github.com/bfwg/ngx-drag-scroll
    appRouting
  ],
  providers: [
    DiagnoseService,
    SymptomService,
    CategoryService,
    TagService,
    ImageService,
    AccountService,
    UserService,
    AuthGuard,
    AlreadyLoggedGuard,
    {provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
