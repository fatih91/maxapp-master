import {Component, OnDestroy, OnInit} from '@angular/core';
import {UserService} from "../../../services/user.service";
import {ISubscription} from "rxjs-compat/Subscription";
import {SymptomService} from "../../../services/symptom.service";

@Component({
  selector: 'max-my-created-symptoms',
  templateUrl: './my-created-symptoms.component.html',
  styleUrls: ['./my-created-symptoms.component.scss']
})
export class MyCreatedSymptomsComponent implements OnInit, OnDestroy {
  symptoms: UserContentResource[];
  private subscription: ISubscription;

  constructor(
    private userService: UserService,
    private symptomService: SymptomService) { }

  ngOnInit() {
    this.subscription = this.userService
      .getUserwithSymptoms()
      .subscribe(user =>
        this.symptoms = user.symptoms)

  }

  deleteSymptom(id: number){
    this.symptomService
      .deleteSymptom(id)
      .subscribe(id =>
        this.symptoms = this.symptoms.filter(s => s.id != id));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }


}
