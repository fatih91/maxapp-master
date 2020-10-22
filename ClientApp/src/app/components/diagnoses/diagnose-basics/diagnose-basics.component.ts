import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {Diagnose} from "../../../models/diagnose.model";

@Component({
  selector: 'max-diagnose-basics',
  templateUrl: './diagnose-basics.component.html',
  styleUrls: ['./diagnose-basics.component.scss'],
  //encapsulation: ViewEncapsulation.None
})
export class DiagnoseBasicsComponent implements OnInit {

  @Input() diagnose: Diagnose;

  constructor() { }

  ngOnInit() {
  }
}
