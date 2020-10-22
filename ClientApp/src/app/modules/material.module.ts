import {NgModule} from '@angular/core';
import {
  MatAutocompleteModule,
  MatButtonModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDialogModule,
  MatExpansionModule,
  MatFormFieldModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatSidenavModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTreeModule,
  MatTooltipModule,
} from "@angular/material";

export const MODULES = [
  MatButtonModule,
  MatCheckboxModule,
  MatInputModule,
  MatIconModule,
  MatToolbarModule,
  MatSidenavModule,
  MatGridListModule,
  MatListModule,
  MatTabsModule,
  MatCardModule,
  MatStepperModule,
  MatExpansionModule,
  MatTableModule,
  MatChipsModule,
  MatSortModule,
  MatFormFieldModule,
  MatAutocompleteModule,
  MatDialogModule,
  MatTreeModule,
  MatTooltipModule
];



@NgModule({
    imports: MODULES,
    exports: MODULES
})
export class MaterialModule {
}
