// // components/client-form/client-form.component.ts
// import { Component, Input, Output, EventEmitter } from '@angular/core';
// import { Client, ClientRole } from '../../services/clients.service';
// import { NzFormItemComponent } from "ng-zorro-antd/form";
// import { NzOptionComponent, NzSelectComponent } from "ng-zorro-antd/select";
// import { NzColDirective } from "ng-zorro-antd/grid";
// import { UploadPictureComponent } from "../../../../shared/file-upload/file-upload.component";
// import { NzDatePickerComponent } from "ng-zorro-antd/date-picker";

// @Component({
//   selector: 'app-client-form',
//   template: `
//     <form nz-form>
//       <div nz-row nzGutter="16">
//         <div nz-col nzXs="24" nzMd="12">
//           <nz-form-item>
//             <nz-form-label nzRequired>الاسم الكامل</nz-form-label>
//             <nz-form-control>
//               <input nz-input [(ngModel)]="client.fullName" name="fullName"
//                      placeholder="أدخل الاسم الكامل" required />
//             </nz-form-control>
//           </nz-form-item>
//         </div>
//         <div nz-col nzXs="24" nzMd="12">
//           <nz-form-item>
//             <nz-form-label nzRequired>تاريخ الميلاد</nz-form-label>
//             <nz-form-control>
//               <nz-date-picker [(ngModel)]="client.birthDate" name="birthDate"></nz-date-picker>
//             </nz-form-control>
//           </nz-form-item>
//         </div>
//       </div>

//       <div nz-row nzGutter="16">
//         <div nz-col nzXs="24" nzMd="12">
//           <nz-form-item>
//             <nz-form-label nzRequired>نوع العميل</nz-form-label>
//             <nz-form-control>
//               <nz-select [(ngModel)]="client.clientType" name="clientType" nzPlaceHolder="اختر نوع العميل" required>
//                 <nz-option nzValue="1" nzLabel="فردي"></nz-option>
//                 <nz-option nzValue="2" nzLabel="شركة"></nz-option>
//                 <nz-option nzValue="3" nzLabel="شخص"></nz-option>
//               </nz-select>
//             </nz-form-control>
//           </nz-form-item>
//         </div>
//         <div nz-col nzXs="24" nzMd="12">
//           <nz-form-item>
//             <nz-form-label nzRequired>دور العميل</nz-form-label>
//             <nz-form-control>
//               <nz-select [(ngModel)]="client.clientRoleId" name="clientRoleId" nzPlaceHolder="اختر دور العميل" required>
//                 <nz-option *ngFor="let role of clientRoles"
//                           [nzValue]="role.id" [nzLabel]="role.name"></nz-option>
//               </nz-select>
//               <button nz-button nzType="link" nzSize="small" (click)="onAddRole.emit()" class="mt-1">
//                 <i nz-icon nzType="plus"></i>
//                 إضافة دور جديد
//               </button>
//             </nz-form-control>
//           </nz-form-item>
//         </div>
//       </div>

//       <div nz-row nzGutter="16">
//         <div nz-col nzXs="24" nzMd="12">
//           <nz-form-item>
//             <nz-form-label>البريد الإلكتروني</nz-form-label>
//             <nz-form-control>
//               <input nz-input type="email" [(ngModel)]="client.email"
//                      name="email" placeholder="أدخل البريد الإلكتروني" />
//             </nz-form-control>
//           </nz-form-item>
//         </div>
//         <div nz-col nzXs="24" nzMd="12">
//           <nz-form-item>
//             <nz-form-label nzRequired>رقم الهاتف</nz-form-label>
//             <nz-form-control>
//               <input nz-input type="tel" [(ngModel)]="client.phoneNumber"
//                      name="phoneNumber" placeholder="أدخل رقم الهاتف" required />
//             </nz-form-control>
//           </nz-form-item>
//         </div>
//       </div>

//       <nz-form-item>
//         <nz-form-label>العنوان</nz-form-label>
//         <nz-form-control>
//           <textarea nz-input [(ngModel)]="client.address" name="address"
//                     rows="3" placeholder="أدخل العنوان الكامل"></textarea>
//         </nz-form-control>
//       </nz-form-item>

//       <nz-form-item>
//         <nz-form-label [nzRequired]="isRequired">صورة الهوية الوطنية</nz-form-label>
//         <nz-form-control>
//           <upload-Document
//             [lable]="'صورة الهوية'"
//             [IsMultiple]="false"
//             [FileUrl]="imageUrl"
//             [uploadFolder]="'Clients'"
//             (fileChanged)="onImageChange.emit($event)">
//           </upload-Document>

//           <!-- عرض الصورة الحالية في وضع التعديل -->
//           <div *ngIf="client.urlImageNationalId && !imageUrl" class="mt-2">
//             <img [src]="client.urlImageNationalId" alt="صورة الهوية الحالية"
//                  class="current-image" />
//             <div class="text-center mt-1">
//               <small nz-typography nzType="secondary">الصورة الحالية</small>
//             </div>
//           </div>

//           <!-- عرض الصورة الجديدة -->
//           <div *ngIf="imageUrl" class="mt-2">
//             <img [src]="imageUrl" alt="صورة الهوية الجديدة"
//                  class="new-image" />
//             <div class="text-center mt-1">
//               <small nz-typography nzType="success">✓ الصورة الجديدة</small>
//             </div>
//           </div>

//           <div class="error-message" *ngIf="showValidationErrors && !imageUrl && isRequired">
//             <span style="color: #ff4d4f; font-size: 12px;">صورة الهوية الوطنية مطلوبة</span>
//           </div>
//         </nz-form-control>
//       </nz-form-item>
//     </form>
//   `,
//   imports: [NzFormItemComponent, NzOptionComponent, NzColDirective, NzSelectComponent, UploadPictureComponent, NzDatePickerComponent]
// })
// export class ClientFormComponent {
//   @Input() client!: Client;
//   @Input() clientRoles: ClientRole[] = [];
//   @Input() imageUrl: string = '';
//   @Input() showValidationErrors: boolean = false;
//   @Input() isRequired: boolean = true;
//   @Input() isEditMode: boolean = false;

//   @Output() onImageChange = new EventEmitter<string>();
//   @Output() onAddRole = new EventEmitter<void>();
// }
