import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'avatar',
})
export class AvatarPipe implements PipeTransform {
  transform(value: string, ...args: unknown[]): unknown {
    console.log(value);
    const nameArray = value.split(' ');
    return nameArray[0].slice(0, 1) + '.' + nameArray[1].slice(0, 1);
  }
}
