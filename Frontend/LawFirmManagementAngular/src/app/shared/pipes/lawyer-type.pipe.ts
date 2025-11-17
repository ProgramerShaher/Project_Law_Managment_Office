import { Pipe, PipeTransform } from '@angular/core';
import { LawyerType } from '../../features/lawyers/services/lawyer.service';

@Pipe({
  name: 'lawyerType',
  standalone: true
})
export class LawyerTypePipe implements PipeTransform {
  transform(type: LawyerType): string {
    const labels = {
      [LawyerType.Trainee]: 'متدرب',
      [LawyerType.Consultant]: 'مستشار',
      [LawyerType.Expert]: 'خبير',
      [LawyerType.Other]: 'أخرى'
    };
    return labels[type] || 'غير محدد';
  }
}
