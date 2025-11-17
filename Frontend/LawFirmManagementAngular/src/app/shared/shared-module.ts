// src/app/shared/shared.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// --- استيراد جميع وحدات NG-ZORRO المطلوبة ---
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzBadgeModule } from 'ng-zorro-antd/badge';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzCarouselModule } from 'ng-zorro-antd/carousel';
import { NzCheckboxModule } from 'ng-zorro-antd/checkbox';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzDrawerModule } from 'ng-zorro-antd/drawer';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzFormItemComponent, NzFormModule } from 'ng-zorro-antd/form';
import { NzColDirective, NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzInputNumberModule } from 'ng-zorro-antd/input-number';
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzRadioModule } from 'ng-zorro-antd/radio';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzSkeletonModule } from 'ng-zorro-antd/skeleton';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzStepsModule } from 'ng-zorro-antd/steps';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTabsModule } from 'ng-zorro-antd/tabs';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzTimePickerModule } from 'ng-zorro-antd/time-picker';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzCollapseModule } from 'ng-zorro-antd/collapse';
import { NzUploadChangeParam, NzUploadFile, NzUploadComponent } from 'ng-zorro-antd/upload';

// import { OtelioSelectComponent } from './otelio-select/otelio-select.component';
// import { LazyLocalizationPipe  } from '@abp/ng.core';
// import { LocalizationPipe  } from '@abp/ng.core';
import { RouterLink, RouterModule } from '@angular/router';
import { NzPopoverModule } from 'ng-zorro-antd/popover';
import { NzStatisticModule } from "ng-zorro-antd/statistic";
import { NzSpinModule } from 'ng-zorro-antd/spin';
// قائمة مجمعة لجميع وحدات NG-ZORRO لتسهيل إدارتها
const ZORRO_MODULES = [
  RouterModule,NzStatisticModule,NzSpinModule,
    NzUploadModule,// NzFormControlComponent, NzFormLabelComponent,
  NzAlertModule,//LazyLocalizationPipe ,LocalizationPipe,
  NzPopoverModule,
   NzFormItemComponent,
   NzColDirective,
  NzUploadComponent,
  NzAvatarModule,
  NzBadgeModule,
  NzCollapseModule,
  NzBreadCrumbModule,
  NzButtonModule,
  NzCardModule,
  NzCarouselModule,
  NzCheckboxModule,
  NzDatePickerModule,
  NzDescriptionsModule,
  NzDividerModule,
  NzDrawerModule,
  NzDropDownModule,
  NzEmptyModule,
  NzFormModule,
  NzGridModule,
  NzIconModule,
  NzInputModule,
  NzInputNumberModule,
  NzLayoutModule,
  NzListModule,
  NzMenuModule,
  NzModalModule,
  NzPaginationModule,
  NzPopconfirmModule,
  NzRadioModule,
  NzSelectModule,
  NzSkeletonModule,
  NzSpaceModule,
  NzStepsModule,
  NzTableModule,
  NzTabsModule,
  NzTagModule,
  NzTimePickerModule,
  NzToolTipModule,

];

@NgModule({
  declarations: [
    // ضع هنا المكونات والتوجيهات والأنابيب المشتركة التي ستقوم بإنشائها
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  exports: [
    // قم بتصدير كل شيء حتى تتمكن الوحدات الأخرى من استخدامه
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    // قم بتصدير المكونات والتوجيهات والأنابيب المشتركة هنا أيضًا
  ]
})
export class SharedModule { }
