import {Component, OnDestroy, OnInit} from '@angular/core';
import {Diagnose} from "../../../models/diagnose.model";
import {UserService} from "../../../services/user.service";
import {ISubscription} from "rxjs-compat/Subscription";
import {DiagnoseService} from "../../../services/diagnose.service";

@Component({
  selector: 'max-my-diagnoses-list',
  templateUrl: './my-diagnoses-list.component.html',
  styleUrls: ['./my-diagnoses-list.component.scss']
})
export class MyDiagnosesListComponent implements OnInit, OnDestroy {
  diagnoses: UserContentResource[];
  private subscription: ISubscription;

  constructor(
    private userService: UserService,
    private diagnoseService: DiagnoseService

) { }

  ngOnInit() {
    this.subscription = this.userService
      .getUserWithDiagnoses()
      .subscribe(user => {
        this.diagnoses = user.diagnoses;
        console.log(user.diagnoses);
      });
  }

  deleteDiagnose(id: number){
    this.diagnoseService
      .deleteDiagnose(id).subscribe(id => {
        this.diagnoses = this.diagnoses
          .filter(d => d.id != id);
    });

  }
//ToDO: Recherche: Wie handle ich mit mehreren Subscription???
  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

}
