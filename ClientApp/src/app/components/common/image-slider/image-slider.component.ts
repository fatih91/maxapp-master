import {
  AfterContentInit,
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
  ViewEncapsulation
} from '@angular/core';
import {animate, state, style, transition, trigger} from "@angular/animations";
import {Image} from "../../../models/image.model";
import {Observable} from "rxjs/Observable";

@Component({
  selector: 'max-image-slider',
  templateUrl: './image-slider.component.html',
  styleUrls: ['./image-slider.component.scss'],
  animations: [
    trigger('imageState', [
      state('active', style({ 'opacity': '1'})),
      state('inactive', style({ 'opacity': '0'})),
      transition('active => inactive', [animate('350ms ease-out')]),
      transition('inactive => active', [animate('350ms ease-in')]),
    ])
  ]
  //encapsulation: ViewEncapsulation.None
})
export class ImageSliderComponent
{
  @Input()
  images: Image[] = [];
  imageUrl: any;

  public selectedImage: Image;

  imageActive: boolean;

  constructor() {
    this.imageActive = true;
  }

  ngOnInit(): void {
    setTimeout(() => {
      this.selectedImage = this.images[0];
      console.log(this.images);
    }, 350);
  }

  clickItem(image: Image){
    this.imageActive = false;
    console.log(image);
    setTimeout(()=> {this.selectedImage = image; this.imageActive = true; }, 350);
  }
}
