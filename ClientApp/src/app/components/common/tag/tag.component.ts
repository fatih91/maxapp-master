import {Component, Input, OnInit} from '@angular/core';
import {Tag} from "../../../models/tag.model";


@Component({
  selector: 'max-tag',
  templateUrl: './tag.component.html',
  styleUrls: ['./tag.component.scss']
})
export class TagComponent {
  @Input() tags : Tag[];
}
