import { PipeTransform, Pipe } from '@angular/core';

@Pipe({name: 'daysOfWeek'})
export class DaysOfWeekPipe implements PipeTransform {
  transform(value: any, ...args: any[]) {
    const days = [];
    if (value instanceof Array) {
     for (const item in value) {
       if (!item) { continue; }
       const val = Number(item);
       if (isNaN(val)) {
        continue;
       }
       days.push(this.GetDay(val));
     }
     return days.join(', ');
    } else {
      const val = Number(value);
      if (!isNaN(val)) {
        return this.GetDay(val);
      }
    }
    return '';
  }
  private GetDay(day: number) {
    switch (day) {
      case 1: return 'Poniedziałek';
      case 2: return 'Wtorek';
      case 3: return 'Środa';
      case 4: return 'Czwartek';
      case 5: return 'Piątek';
      case 6: return 'Sobota';
      case 7: return 'Niedziela';
      default: return '';
    }
  }
}
