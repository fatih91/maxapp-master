import { Pipe, PipeTransform } from '@angular/core';
import {KeyValuePair} from "../models/keyValuePair.model";

@Pipe({name: 'arrayToString'})
export class ArrayToStringPipe implements PipeTransform {

  transform(value: KeyValuePair[]): string {

    if(value.length > 0)
    {

      let result : string = "";
      for (let i = 0; i < value.length - 1; i++) {
        result+= value[i].value;
        result+=", ";
      }
      result += value[value.length - 1].value;
      return result;
    }
  }
}
