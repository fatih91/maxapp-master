import {DiagnoseLayoutComponent} from "../components/layouts/diagnose-layout.component";
import {MyDiagnosesListComponent} from "../components/diagnoses/my-diagnoses-list/my-diagnoses-list.component";
import {DiagnoseCreateComponent} from "../components/diagnoses/diagnose-create/diagnose-create.component";

export const myDiagnosesRoutes = [{
  path: '', component: DiagnoseLayoutComponent,
  children: [
    {path: '', component:MyDiagnosesListComponent},
    {path: 'new', component: DiagnoseCreateComponent},
    {path: 'edit/:id', component: DiagnoseCreateComponent},
  ]
}];
