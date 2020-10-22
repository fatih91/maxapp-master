import { Pipe, PipeTransform } from '@angular/core';
import {Image} from "../models/image.model";

@Pipe({name: 'firstElementOfImage'})
export class FirstElementOfImagesPipe implements PipeTransform {

  transform(value: Image[]): string {

    if(value)
    {
      if (value[0] != null)
      return value[0].fileName;
    }
  }
}
