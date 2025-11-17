import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

import { Lawyer, LawyerCreate, LawyerService, LawyerType } from '../../services/lawyer.service';
import { NzModalModule } from 'ng-zorro-antd/modal';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule } from 'ng-zorro-antd/upload';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { UploadPictureComponent } from '../../../../shared/file-upload/file-upload.component';


@Component({
  selector: 'app-lawyer-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzModalModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzDatePickerModule,
    NzUploadModule,
    NzButtonModule,
    NzIconModule,
    UploadPictureComponent
  ],
  template: `
    <nz-modal
      [(nzVisible)]="isVisible"
      [nzTitle]="isEditMode ? 'تعديل بيانات المحامي' : 'إضافة محامي جديد'"
      nzWidth="800px"
      nzWrapClassName="arabic-modal"
      (nzOnCancel)="onCancel()"
      [nzFooter]="null">
      <ng-container *nzModalContent>
        <form nz-form class="arabic-form" #lawyerForm="ngForm">
          <!-- Debug Panel -->
          <div *ngIf="showDebug" style="background: #fff3cd; padding: 10px; margin: 10px 0; border: 1px solid #ffeaa7; border-radius: 4px; direction: ltr;">
            <h4 style="margin: 0 0 8px 0; color: #856404;">Debug Information:</h4>
            <p style="margin: 2px 0; font-size: 12px;"><strong>FullName:</strong> "{{formData.fullName}}" (length: {{formData.fullName?.length}})</p>
            <p style="margin: 2px 0; font-size: 12px;"><strong>PhoneNumber:</strong> "{{formData.phoneNumber}}" (length: {{formData.phoneNumber?.length}})</p>
            <p style="margin: 2px 0; font-size: 12px;"><strong>Address:</strong> "{{formData.address}}" (length: {{formData.address?.length}})</p>
            <p style="margin: 2px 0; font-size: 12px;"><strong>Type:</strong> "{{formData.type}}"</p>
            <p style="margin: 2px 0; font-size: 12px;"><strong>Email:</strong> "{{formData.email}}"</p>
            <p style="margin: 2px 0; font-size: 12px;"><strong>DateOfBirth:</strong> "{{formData.dateOfBirth}}"</p>
            <p style="margin: 2px 0; font-size: 12px;"><strong>Identity Image:</strong> "{{identityImagePath}}"</p>
             <p style="margin: 2px 0; font-size: 12px;"><strong> qualificationDocumentPath:</strong> "{{qualificationDocumentPath}}"</p>

          </div>

          <div nz-row nzGutter="16">
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label nzRequired>الاسم الكامل</nz-form-label>
                <nz-form-control>
                  <input
                    nz-input
                    [(ngModel)]="formData.fullName"
                    name="fullName"
                    placeholder="أدخل الاسم الكامل"
                    required
                    (blur)="trimField('fullName')"
                  />
                  <div class="error-message" *ngIf="showValidationErrors && !formData.fullName?.trim()">
                    <span style="color: #ff4d4f; font-size: 12px;">هذا الحقل مطلوب</span>
                  </div>
                </nz-form-control>
              </nz-form-item>
            </div>
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label nzRequired>رقم الهاتف</nz-form-label>
                <nz-form-control>
                  <input
                    nz-input
                    [(ngModel)]="formData.phoneNumber"
                    name="phoneNumber"
                    placeholder="أدخل رقم الهاتف"
                    required
                    (blur)="trimField('phoneNumber')"
                  />
                  <div class="error-message" *ngIf="showValidationErrors && !formData.phoneNumber?.trim()">
                    <span style="color: #ff4d4f; font-size: 12px;">هذا الحقل مطلوب</span>
                  </div>
                </nz-form-control>
              </nz-form-item>
            </div>
          </div>

          <div nz-row nzGutter="16">
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label>البريد الإلكتروني</nz-form-label>
                <nz-form-control>
                  <input
                    nz-input
                    type="email"
                    [(ngModel)]="formData.email"
                    name="email"
                    placeholder="أدخل البريد الإلكتروني"
                    (blur)="trimField('email')"
                  />
                </nz-form-control>
              </nz-form-item>
            </div>
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label>تاريخ الميلاد</nz-form-label>
                <nz-form-control>
                  <nz-date-picker
                    [(ngModel)]="formData.dateOfBirth"
                    name="dateOfBirth"
                    nzFormat="yyyy-MM-dd"
                    class="full-width"
                    placeholder="اختر تاريخ الميلاد">
                  </nz-date-picker>
                </nz-form-control>
              </nz-form-item>
            </div>
          </div>

          <nz-form-item>
            <nz-form-label nzRequired>العنوان</nz-form-label>
            <nz-form-control>
              <textarea
                nz-input
                [(ngModel)]="formData.address"
                name="address"
                placeholder="أدخل العنوان الكامل"
                rows="3"
                required
                (blur)="trimField('address')"
              ></textarea>
              <div class="error-message" *ngIf="showValidationErrors && !formData.address?.trim()">
                <span style="color: #ff4d4f; font-size: 12px;">هذا الحقل مطلوب</span>
              </div>
              <div class="error-message" *ngIf="showValidationErrors && formData.address?.trim() && formData.address.trim().length < 5">
                <span style="color: #ff4d4f; font-size: 12px;">العنوان يجب أن يكون على الأقل 5 أحرف</span>
              </div>
            </nz-form-control>
          </nz-form-item>

          <div nz-row nzGutter="16">
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label nzRequired>نوع المحامي</nz-form-label>
                <nz-form-control>
                  <nz-select
                    [(ngModel)]="formData.type"
                    name="type"
                    class="full-width"
                    nzPlaceHolder="اختر نوع المحامي">
                    <nz-option nzValue="1" nzLabel="متدرب"></nz-option>
                    <nz-option nzValue="2" nzLabel="مستشار"></nz-option>
                    <nz-option nzValue="3" nzLabel="خبير"></nz-option>
                    <nz-option nzValue="4" nzLabel="أخرى"></nz-option>
                  </nz-select>
                  <div class="error-message" *ngIf="showValidationErrors && !formData.type">
                    <span style="color: #ff4d4f; font-size: 12px;">هذا الحقل مطلوب</span>
                  </div>
                </nz-form-control>
              </nz-form-item>
            </div>
          </div>

          <!-- صورة الهوية الوطنية - مطلوبة -->
          <div nz-row nzGutter="16">
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label nzRequired>صورة الهوية الوطنية</nz-form-label>
                <nz-form-control>
                  <upload-Document
                    [lable]="'صورة الهوية'"
                    [IsMultiple]="false"
                    [FileUrl]="identityImagePath"
                    [uploadFolder]="'Lawyers'"
                    (fileChanged)="onIdentityImageChanged($event)"
                    (fileObjectChanged)="onIdentityImageFileChanged($event)">
                  </upload-Document>
                  <div class="error-message" *ngIf="showValidationErrors && !identityImagePath && !isEditMode">
                    <span style="color: #ff4d4f; font-size: 12px;">صورة الهوية الوطنية مطلوبة</span>
                  </div>
                </nz-form-control>
              </nz-form-item>
            </div>
            <div nz-col nzSpan="12">
              <nz-form-item>
                <nz-form-label>وثائق المؤهلات العلمية</nz-form-label>
                <nz-form-control>
                  <upload-Document
                    [lable]="'وثائق المؤهلات'"
                    [IsMultiple]="false"
                    [FileUrl]="qualificationDocumentPath"
                    [uploadFolder]="'Lawyers'"
                    (fileChanged)="onQualificationDocumentChanged($event)"
                    (fileObjectChanged)="onQualificationDocumentFileChanged($event)">
                  </upload-Document>
                </nz-form-control>
              </nz-form-item>
            </div>
          </div>

          <div nz-row nzJustify="end" class="form-actions">
            <div nz-col>
              <button
                nz-button
                nzType="default"
                (click)="toggleDebug()"
                class="action-btn"
                type="button">
                {{ showDebug ? 'إخفاء التصحيح' : 'عرض التصحيح' }}
              </button>
              <button
                nz-button
                nzType="default"
                (click)="onCancel()"
                class="action-btn"
                type="button">
                إلغاء
              </button>
              <button
                nz-button
                nzType="primary"
                (click)="onSubmit()"
                [nzLoading]="isSubmitting"
                class="action-btn"
                type="button">
                {{ isEditMode ? 'تحديث البيانات' : 'حفظ المحامي' }}
              </button>
            </div>
          </div>
        </form>
      </ng-container>
    </nz-modal>
  `,
  styleUrls: ['./lawyer-form.component.scss']
})
export class LawyerFormComponent implements OnInit {
  @Input() isVisible: boolean = false;
  @Input() lawyer: Lawyer | null = null;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() lawyerSaved = new EventEmitter<Lawyer>();

  formData: any = {
    fullName: '',
    phoneNumber: '',
    address: '',
    type: LawyerType.Trainee.toString(),
    email: '',
    dateOfBirth: null
  };

  identityImagePath: string = '';
  qualificationDocumentPath: string = '';
  // hold original File objects from upload component so we can attach them to FormData
  identityFile: any = null;
  qualificationFile: any = null;
  isSubmitting: boolean = false;
  isEditMode: boolean = false;
  showValidationErrors: boolean = false;
  showDebug: boolean = true;

  constructor(
    private lawyerService: LawyerService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    if (this.lawyer) {
      this.isEditMode = true;
      this.formData = {
        fullName: this.lawyer.fullName || '',
        phoneNumber: this.lawyer.phoneNumber || '',
        address: this.lawyer.address || '',
        type: this.lawyer.type?.toString() || LawyerType.Trainee.toString(),
        email: this.lawyer.email || '',
        dateOfBirth: this.lawyer.dateOfBirth ? new Date(this.lawyer.dateOfBirth) : null
      };

      this.identityImagePath = this.lawyer.identityImagePath || '';
      this.qualificationDocumentPath = this.lawyer.qualificationDocumentPath || '';

      console.log('تم تحميل بيانات المحامي للتحرير:', this.formData);
    }
  }

  trimField(fieldName: string): void {
    if (this.formData[fieldName]) {
      this.formData[fieldName] = this.formData[fieldName].trim();
    }
  }

  toggleDebug(): void {
    this.showDebug = !this.showDebug;
  }

  onIdentityImageChanged(fileUrl: string): void {
    console.log('تم رفع صورة الهوية:', fileUrl);
    this.identityImagePath = fileUrl;

    if (fileUrl) {
      this.message.success('تم رفع صورة الهوية بنجاح');
    }
  }

  onIdentityImageFileChanged(fileObj: any): void {
    console.log('Identity file object changed:', fileObj);
    this.identityFile = fileObj;
  }

  onQualificationDocumentChanged(fileUrl: string): void {
    console.log('تم رفع وثائق المؤهلات:', fileUrl);
    this.qualificationDocumentPath = fileUrl;

    if (fileUrl) {
      this.message.success('تم رفع وثائق المؤهلات بنجاح');
    }
  }

  onQualificationDocumentFileChanged(fileObj: any): void {
    console.log('Qualification file object changed:', fileObj);
    this.qualificationFile = fileObj;
  }

  onSubmit(): void {
    this.showValidationErrors = true;

    // تنظيف جميع الحقول قبل التحقق
    this.trimAllFields();

    // التحقق من صحة النموذج
    const validation = this.validateFormData();
    if (!validation.isValid) {
      this.message.error('يرجى تصحيح الأخطاء التالية: ' + validation.errors.join('، '));
      return;
    }

    this.isSubmitting = true;

    // إرسال JSON مشابه لطريقة إنشاء العملاء (ملفات قد تكون مرفوعة مسبقاً وإرسال مساراتها)
    const payload = this.createLawyerData();

    console.log('بيانات المحامي المرسلة إلى API (JSON):', payload);

    if (this.isEditMode && this.lawyer?.id) {
      this.updateLawyer(this.lawyer.id, payload);
    } else {
      this.createLawyer(payload);
    }
  }

  private trimAllFields(): void {
    this.formData.fullName = this.formData.fullName?.trim() || '';
    this.formData.phoneNumber = this.formData.phoneNumber?.trim() || '';
    this.formData.address = this.formData.address?.trim() || '';
    this.formData.email = this.formData.email?.trim() || '';
  }

  private validateFormData(): { isValid: boolean; errors: string[] } {
    const errors: string[] = [];

    // التحقق من الحقول المطلوبة
    if (!this.formData.fullName) {
      errors.push('الاسم الكامل مطلوب');
    } else if (this.formData.fullName.length < 2) {
      errors.push('الاسم الكامل يجب أن يكون على الأقل حرفين');
    }

    if (!this.formData.phoneNumber) {
      errors.push('رقم الهاتف مطلوب');
    } else if (this.formData.phoneNumber.length < 5) {
      errors.push('رقم الهاتف يجب أن يكون على الأقل 5 أرقام');
    }

    if (!this.formData.address) {
      errors.push('العنوان مطلوب');
    } else if (this.formData.address.length < 5) {
      errors.push('العنوان يجب أن يكون على الأقل 5 أحرف');
    }

    if (!this.formData.type) {
      errors.push('نوع المحامي مطلوب');
    }

    // في وضع الإضافة، صورة الهوية مطلوبة
    if (!this.isEditMode && !this.identityImagePath) {
      errors.push('صورة الهوية الوطنية مطلوبة');
    }

    // وثائق المؤهلات مطلوبة حسب تحقق السيرفر
    if (!this.qualificationDocumentPath) {
      errors.push('وثائق المؤهلات مطلوبة');
    }

    // البريد الإلكتروني مطلوب حسب نموذج الخادم
    if (!this.formData.email || !this.formData.email.trim()) {
      errors.push('البريد الإلكتروني مطلوب');
    }

    return {
      isValid: errors.length === 0,
      errors: errors
    };
  }

  private createLawyer(lawyerData: any): void {
    this.lawyerService.createLawyer(lawyerData).subscribe({
      next: (createdLawyer) => {
        this.isSubmitting = false;
        this.showValidationErrors = false;
        this.lawyerSaved.emit(createdLawyer);
        this.message.success('تم إضافة المحامي بنجاح');
        this.resetForm();
        this.visibleChange.emit(false);
      },
      error: (error) => {
        this.isSubmitting = false;
        console.error('Error adding lawyer:', error);

        // سجل جسم الخطأ كاملاً لتسهيل التشخيص
        if (error.error) {
          console.error('API error payload:', error.error);
        }

        // معالجة أخطاء التحقق من الصحة — مرّر الجسم الكامل للمعالجة المرنة
        if (error.status === 400) {
          this.handleValidationErrors(error.error);
        } else if (error.status === 500) {
          this.message.error('خطأ في السيرفر. يرجى المحاولة مرة أخرى');
        } else {
          this.message.error('فشل في إضافة المحامي');
        }
      }
    });
  }

  private updateLawyer(id: number, lawyerData: any): void {
    this.lawyerService.updateLawyer(id, lawyerData).subscribe({
      next: (updatedLawyer) => {
        this.isSubmitting = false;
        this.showValidationErrors = false;
        this.lawyerSaved.emit(updatedLawyer);
        this.message.success('تم تحديث بيانات المحامي بنجاح');
        this.resetForm();
        this.visibleChange.emit(false);
      },
      error: (error) => {
        this.isSubmitting = false;
        console.error('Error updating lawyer:', error);

        if (error.error) {
          console.error('API error payload:', error.error);
        }

        if (error.status === 400) {
          this.handleValidationErrors(error.error);
        } else {
          this.message.error('فشل في تحديث بيانات المحامي');
        }
      }
    });
  }

  private createLawyerData(): LawyerCreate {
    // تنظيف البيانات بشكل نهائي
    const cleanData: any = {
      fullName: this.formData.fullName || '',
      phoneNumber: this.formData.phoneNumber || '',
      address: this.formData.address || '',
      type: Number(this.formData.type) || LawyerType.Trainee,
      email: this.formData.email || ''
    };

    // الملفات (مسارات الصور/الوثائق)
    cleanData.identityImagePath = this.identityImagePath || '/uploads/lawyers/default-identity.jpg';
    cleanData.qualificationDocumentPath = this.qualificationDocumentPath || '/uploads/lawyers/default-qualification.pdf';

    // إضافة تاريخ الميلاد إذا كان موجوداً
    if (this.formData.dateOfBirth) {
      const birthDate = new Date(this.formData.dateOfBirth);
      cleanData.dateOfBirth = birthDate.toISOString();
    }

    // شكل JSON مطابق لنموذج الـ C# في الـ backend (PascalCase)
    const payload: any = {
      FullName: cleanData.fullName || '',
      PhoneNumber: cleanData.phoneNumber || '',
      Address: cleanData.address || '',
      Email: cleanData.email || '',
      Type: Number(cleanData.type) || LawyerType.Trainee,
      IdentityImagePath: this.identityImagePath || cleanData.identityImagePath || '',
      QualificationDocumentsPath: this.qualificationDocumentPath || cleanData.qualificationDocument || ''
    };

    if (cleanData.dateOfBirth) {
      payload.DateOfBirth = cleanData.dateOfBirth;
    }

    // التحقق النهائي من البيانات
    console.log('البيانات النهائية المرسلة:', payload);
    const addr = payload.Address || '';
    console.log('تحقق العنوان:', {
      value: addr,
      length: addr.length,
      isEmpty: addr === '',
      isWhitespace: addr.trim() === ''
    });

    return payload;
  }

  // بناء FormData من الـ payload — يستخدم أسماء الحقول بنمط PascalCase كما قد يتوقع الـ API
  private buildFormDataPayload(payload: any): FormData {
    const fd = new FormData();

    // الحقول الأساسية
    fd.append('FullName', payload.FullName || payload.fullName || '');
    fd.append('PhoneNumber', payload.PhoneNumber || payload.phoneNumber || '');
    fd.append('Address', payload.Address || payload.address || '');
    fd.append('Type', String(payload.Type ?? payload.type ?? LawyerType.Trainee));
    fd.append('Email', payload.Email || payload.email || '');

    if (payload.DateOfBirth || payload.dateOfBirth) {
      fd.append('DateOfBirth', payload.DateOfBirth || payload.dateOfBirth);
    }

    // ملفات/مسارات الهوية والمؤهلات — استخدم الاسم الذي ظهر في أخطاء الـ API
    // API يتوقع "IdentityImagePath" و "QualificationDocument" حسب رسالة الخطأ
    // إذا كان المستخدم رفع الملف ويمكننا الوصول لكائن الملف الأصلي، أرفق الملف (IFormFile)
    if (this.identityFile) {
      // NzUploadFile قد يحتوي originFileObj (الملف الفعلي)
      const fileObj = (this.identityFile as any).originFileObj || this.identityFile;
      try {
        // أضف الملف تحت عدة أسماء محتملة لالتقاط أي توقعات مختلفة في الـ API
        fd.append('IdentityImagePath', fileObj, fileObj.name || 'identity');
        fd.append('IdentityImage', fileObj, fileObj.name || 'identity');
        fd.append('IdentityFile', fileObj, fileObj.name || 'identity');
      } catch (e) {
        // إن فشل الإلحاق كملف، أرسل كقيمة نصية بدلًا من ذلك
        const val = payload.IdentityImagePath || payload.identityImagePath || '';
        fd.append('IdentityImagePath', val);
        fd.append('IdentityImage', val);
      }
    } else if (payload.IdentityImagePath || payload.identityImagePath) {
      const val = payload.IdentityImagePath || payload.identityImagePath;
      fd.append('IdentityImagePath', val);
      fd.append('IdentityImage', val);
    }

    // QualificationDocument — قد يكون ملف أو مسار نصي
    if (this.qualificationFile) {
      const qFileObj = (this.qualificationFile as any).originFileObj || this.qualificationFile;
      try {
        // أضف الملف تحت أسماء محتملة
        fd.append('QualificationDocumentPath', qFileObj, qFileObj.name || 'qualification');
      } catch (e) {
        const val = payload.QualificationDocumentPath || payload.QualificationDocumentPath || payload.qualificationDocument || '';
        fd.append('QualificationDocumentPath', val);
      }
    } else if (payload.QualificationDocument || payload.QualificationDocumentPath || payload.qualificationDocument) {
      const val = payload.QualificationDocument || payload.QualificationDocumentPath || payload.qualificationDocument;
      fd.append('QualificationDocumentPath', val);
    }

    return fd;
  }

  private handleValidationErrors(errorPayload: any): void {
    console.error('Validation errors from API payload:', errorPayload);
    try {
      console.error('Validation payload (stringified):', JSON.stringify(errorPayload));
    } catch (e) {
      console.error('Could not stringify error payload', e);
    }

    // شكل شائع: { type, title, status, errors: { FieldName: ["msg1","msg2"] } }
    if (errorPayload && typeof errorPayload === 'object') {
      const errorMessages: string[] = [];

      // إن وُجِد حقل errors ككائن، اجمع الرسائل منه
      if (errorPayload.errors && typeof errorPayload.errors === 'object') {
        for (const key in errorPayload.errors) {
          if (Object.prototype.hasOwnProperty.call(errorPayload.errors, key)) {
            const fieldErrors = errorPayload.errors[key];
            if (Array.isArray(fieldErrors)) {
              errorMessages.push(...fieldErrors);
            } else if (typeof fieldErrors === 'string') {
              errorMessages.push(fieldErrors);
            }
          }
        }
      }

      // بعض الـ APIs ترجع رسالة بدلًا من كائن errors
      if (errorPayload.message && typeof errorPayload.message === 'string') {
        errorMessages.push(errorPayload.message);
      }

      // إن لم نجد رسائل بعد، حاوِل تفكيك أي كائن بسيط
      if (errorMessages.length === 0) {
        for (const key in errorPayload) {
          if (Object.prototype.hasOwnProperty.call(errorPayload, key)) {
            const v = errorPayload[key];
            if (typeof v === 'string') errorMessages.push(v);
            else if (Array.isArray(v)) errorMessages.push(...v.filter(x => typeof x === 'string'));
          }
        }
      }

      if (errorMessages.length > 0) {
        // اعرض أول 5 رسائل لتفادي طول الرسائل
        this.message.error('أخطاء في البيانات: ' + errorMessages.slice(0, 5).join('، '));
        console.warn('All validation messages:', errorMessages);
        return;
      }

      // إذا وصلنا هنا ولم نجمّع رسائل، اطبع مفصل لمفتاح errors إن وجد
      if (errorPayload && errorPayload.errors) {
        console.error('Validation error keys and values:');
        for (const k in errorPayload.errors) {
          if (Object.prototype.hasOwnProperty.call(errorPayload.errors, k)) {
            console.error(k, errorPayload.errors[k]);
          }
        }
      }
    }

    this.message.error('بيانات غير صالحة. يرجى التحقق من المدخلات');
  }

  private resetForm(): void {
    this.formData = {
      fullName: '',
      phoneNumber: '',
      address: '',
      type: LawyerType.Trainee.toString(),
      email: '',
      dateOfBirth: null
    };
    this.identityImagePath = '';
    this.qualificationDocumentPath = '';
    this.showValidationErrors = false;
  }

  onCancel(): void {
    this.visibleChange.emit(false);
    this.resetForm();
    this.showValidationErrors = false;
  }
}
