import {HomeLayoutComponent} from "../components/layouts/home-layout.component";
import {HomeComponent} from "../components/home/home.component";
import {DiagnoseViewComponent} from "../components/diagnoses/diagnose-view/diagnose-view.component";
import {SymptomViewComponent} from "../components/symptoms/symptom-view/symptom-view.component";
import {myDiagnosesRoutes} from "./my-diagnoses.routing";
import {mySymptomsRoutes} from "./my-symptoms.routing";
import {DifferentialDiagnoseTableComponent} from "../components/diagnoses/differentialdiagnose-table/differentialdiagnose-table.component";
import {SymptomTableComponent} from "../components/diagnoses/symptom-table/symptom-table.component";
import {CategoriesViewComponent} from "../components/categories/categories-view/categories-view.component";

export const homeRoutes = [{
    path: '', component: HomeLayoutComponent,
    children: [
      {path: '', component: HomeComponent},
      {path: 'diagnose/:id', component: DiagnoseViewComponent},
      {path: 'symptom/:id', component: SymptomViewComponent},
      {path: 'mydiagnoses', children: myDiagnosesRoutes},
      {path: 'mysymptoms', children: mySymptomsRoutes},
      {path: 'categories', component: CategoriesViewComponent}

      ]
}];

export const homeRoutingComponents = [
  HomeLayoutComponent,
  HomeComponent,
  DiagnoseViewComponent,
  SymptomViewComponent,
  DifferentialDiagnoseTableComponent,
  SymptomTableComponent
];
