import {MyCreatedSymptomsComponent} from "../components/symptoms/my-created-symptoms/my-created-symptoms.component";
import {SymptomCreateComponent} from "../components/symptoms/symptom-create/symptom-create.component";
import {SymptomLayoutComponent} from "../components/layouts/symptom-layout.component";

export const mySymptomsRoutes = [{
  path: '', component: SymptomLayoutComponent,
  children: [
    {path: '', component: MyCreatedSymptomsComponent},
    {path: 'new', component: SymptomCreateComponent},
    {path: 'edit/:id', component: SymptomCreateComponent},
  ]
}];
