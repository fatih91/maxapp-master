import { Pipe, PipeTransform } from '@angular/core';
import {KeyValuePair} from "../models/keyValuePair.model";

@Pipe({name: 'firstElement'})
export class FirstElementPipe implements PipeTransform {

  transform(value: KeyValuePair[]): string {

    if(value)
    {
      if (value[0] != null)
      return value[0].value;
    }
  }
}
