import {Component, OnInit, ViewChild} from '@angular/core';
import {Subscription} from "rxjs/Subscription";
import {Diagnose} from "../../../models/diagnose.model";
import {DiagnoseService} from "../../../services/diagnose.service";
import {ActivatedRoute, Params} from "@angular/router";
import { Location } from '@angular/common';
import {MatAccordion} from "@angular/material";
import {DiagnoseSharedService} from "../diagnose-shared.service";

@Component({
  selector: 'max-diagnose-view',
  templateUrl: 'diagnose-view.component.html',
  styleUrls: ['diagnose-view.component.scss'],
  //encapsulation: ViewEncapsulation.None,
})

export class DiagnoseViewComponent implements OnInit {
    subscription: Subscription;
    diagnose: Diagnose = this.createInitialDiagnose();
    @ViewChild(MatAccordion) matAccordion: MatAccordion;

    constructor(
        private diagnoseService: DiagnoseService,
        private route: ActivatedRoute,
        private location: Location,
        private diagnoseSharedService: DiagnoseSharedService
    ) {}

    ngOnInit(): void {
      this.getDiagnose();
    }

    createInitialDiagnose():Diagnose
    {
      return {
        checklists: [],
        prognoses: [],
        therapies: [],
        differentialdiagnoses: [],
        symptoms:[],
        icds: [],
        synonyms: [],
        diagnostics: [],
        tags: []
      } as Diagnose;
    }



    getDiagnose(): void {
      this.subscription = this.route.params
        .subscribe(params => {
          const id = (params['id'] || '');
          this.diagnoseService.getDiagnose(id)
            .subscribe(d => {
              this.diagnose = d as Diagnose;
              this.diagnoseSharedService.setDiagnoseSource(d);
            });
        });
    }

    goBack():void {
      this.location.back();
    }

    expansionPanel():boolean{
      return this.diagnose.checklists.length > 0 ||
        this.diagnose.therapies.length > 0 ||
        this.diagnose.prognoses.length > 0 ||
        this.diagnose.symptoms.length > 0 ||
        this.diagnose.differentialdiagnoses.length > 0;
    }
}
