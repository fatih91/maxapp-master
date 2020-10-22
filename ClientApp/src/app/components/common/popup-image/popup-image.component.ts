import {Component, Inject, OnInit, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";
import {ImageDialogComponent} from "../image-dialog/image-dialog.component";
import {ImageService} from "../../../services/image.service";
import {Observable} from "rxjs/Observable";
import {Image} from "../../../models/image.model";
import {ImageSliderComponent} from "../image-slider/image-slider.component";

@Component({
  selector: 'max-popup-image',
  templateUrl: './popup-image.component.html',
  styleUrls: ['./popup-image.component.scss']
})
export class PopupImageComponent implements OnInit {
  public selectedImages: Image[] = [];
  public postSelectedImages: Image;
  @ViewChild("imageSlider") public slider: ImageSliderComponent;

  constructor(
    public dialogRef: MatDialogRef<ImageDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any[],
    private imageService: ImageService) {
  }

  ngOnInit() {
    this.data[0].forEach(id => {
      this.imageService.getImages(id).subscribe(images =>{
        this.selectedImages.push(...images)
      });

    });
  }

  saveImage(){
    this.dialogRef.close(this.slider.selectedImage);
  }
}
