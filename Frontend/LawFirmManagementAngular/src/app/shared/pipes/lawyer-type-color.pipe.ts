import { Pipe, PipeTransform } from '@angular/core';
import { LawyerType } from '../../features/lawyers/services/lawyer.service';

@Pipe({
  name: 'lawyerTypeColor',
  standalone: true
})
export class LawyerTypeColorPipe implements PipeTransform {
  transform(type: LawyerType): string {
    const colors = {
      [LawyerType.Trainee]: 'blue',
      [LawyerType.Consultant]: 'green',
      [LawyerType.Expert]: 'orange',
      [LawyerType.Other]: 'purple'
    };
    return colors[type] || 'default';
  }
}
