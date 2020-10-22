import { Pipe, PipeTransform } from '@angular/core';
import {KeyValuePair} from "../models/keyValuePair.model";

@Pipe({name: 'int2Gender'})
export class Int2GenderPipe implements PipeTransform {

  transform(value: number): string {

    let result: string;

      if (value == 0){
        result = "Männlich";
      }
      else if (value == 1){
        result = "Weiblich";
      }
      return result;

  }
}
