import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { NzUploadModule, NzUploadChangeParam, NzUploadFile } from 'ng-zorro-antd/upload';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzSwitchModule } from 'ng-zorro-antd/switch';
import { NzRadioModule } from 'ng-zorro-antd/radio';

// النماذج
interface Agency {
  id?: number;
  agencyNumber: string;
  issueDate: Date;
  expiryDate?: Date;
  issuingAuthority: string;
  clientId: number;
  officeID?: number;
  lawyerID?: number;
  agencyType: string;
  derivedPowerOfAttorney: boolean;
  documentFile?: string;
  createdAt?: Date;
  updatedAt?: Date;
}

interface Client {
  id: number;
  fullName: string;
  phoneNumber: string;
}

interface Lawyer {
  id: number;
  fullName: string;
  type: string;
}

@Component({
  selector: 'app-agency-management',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzTableModule,
    NzButtonModule,
    NzModalModule,
    NzFormModule,
    NzInputModule,
    NzSelectModule,
    NzDatePickerModule,
    NzUploadModule,
    NzCardModule,
    NzTagModule,
    NzStatisticModule,
    NzGridModule,
    NzIconModule,
    NzAvatarModule,
    NzToolTipModule,
    NzPaginationModule,
    NzEmptyModule,
    NzSwitchModule,
    NzRadioModule
  ],
  templateUrl: './agency-management.component.html',
  styleUrls: ['./agency-management.component.scss']
})
export class AgencyManagementComponent implements OnInit {
  // البيانات
  agencies: Agency[] = [];
  filteredAgencies: Agency[] = [];

  // القوائم المنسدلة
  clients: Client[] = [];
  lawyers: Lawyer[] = [];

  // المكتب (ثابت - مكتب واحد)
  officeId: number = 1;
  officeName: string = 'المكتب الرئيسي';

  // النماذج
  newAgency: Agency = {
    agencyNumber: '',
    issueDate: new Date(),
    expiryDate: undefined,
    issuingAuthority: '',
    clientId: 0,
    officeID: this.officeId,
    lawyerID: undefined,
    agencyType: 'عامة',
    derivedPowerOfAttorney: false
  };

  editedAgency: Agency = { ...this.newAgency };

  // حالة التطبيق
  searchTerm: string = '';
  selectedType: string = '';
  isAddModalVisible = false;
  isEditModalVisible = false;
  loading = false;
  selectedRepresentative: 'office' | 'lawyer' | null = 'office';
  canBeDerived: boolean = false;

  // الباجينيشن
  pageSize = 10;
  pageIndex = 1;
  totalItems = 0;

  // الإحصائيات
  stats = {
    total: 0,
    active: 0,
    expired: 0,
    derived: 0
  };

  constructor(
    private modal: NzModalService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.loadSampleData();
    this.calculateStats();
    this.filteredAgencies = [...this.agencies];
    this.totalItems = this.agencies.length;
  }

  // تحميل بيانات نموذجية
  private loadSampleData(): void {
    // تحميل العملاء
    this.clients = [
      { id: 1, fullName: 'محمد أحمد العلي', phoneNumber: '0501234567' },
      { id: 2, fullName: 'فاطمة خالد السعد', phoneNumber: '0559876543' },
      { id: 3, fullName: 'خالد سعد القحطاني', phoneNumber: '0541122334' },
      { id: 4, fullName: 'نورة راشد الحربي', phoneNumber: '0534455667' }
    ];

    // تحميل المحامين
    this.lawyers = [
      { id: 1, fullName: 'أحمد محمد العلي', type: 'شريك' },
      { id: 2, fullName: 'فاطمة عبدالله السعد', type: 'محامي أول' },
      { id: 3, fullName: 'خالد سعد القحطاني', type: 'محامي مبتدئ' }
    ];

    // تحميل الوكالات
    this.agencies = [
      {
        id: 1,
        agencyNumber: 'AG-2024-001',
        issueDate: new Date('2024-01-15'),
        expiryDate: new Date('2025-01-15'),
        issuingAuthority: 'محكمة الرياض',
        clientId: 1,
        officeID: this.officeId,
        lawyerID: undefined,
        agencyType: 'خاصة',
        derivedPowerOfAttorney: true,
        documentFile: 'agency_1.pdf',
        createdAt: new Date('2024-01-15')
      },
      {
        id: 2,
        agencyNumber: 'AG-2024-002',
        issueDate: new Date('2024-02-20'),
        expiryDate: new Date('2024-08-20'),
        issuingAuthority: 'محكمة جدة',
        clientId: 2,
        officeID: undefined,
        lawyerID: 1,
        agencyType: 'عامة',
        derivedPowerOfAttorney: false,
        documentFile: 'agency_2.pdf',
        createdAt: new Date('2024-02-20')
      },
      {
        id: 3,
        agencyNumber: 'AG-2024-003',
        issueDate: new Date('2024-03-10'),
        expiryDate: new Date('2024-09-10'),
        issuingAuthority: 'محكمة الدمام',
        clientId: 3,
        officeID: this.officeId,
        lawyerID: undefined,
        agencyType: 'خاصة',
        derivedPowerOfAttorney: false,
        createdAt: new Date('2024-03-10')
      }
    ];
  }

  // حساب الإحصائيات
  private calculateStats(): void {
    this.stats.total = this.agencies.length;
    this.stats.active = this.agencies.filter(a =>
      !a.expiryDate || new Date(a.expiryDate) > new Date()
    ).length;
    this.stats.expired = this.agencies.filter(a =>
      a.expiryDate && new Date(a.expiryDate) <= new Date()
    ).length;
    this.stats.derived = this.agencies.filter(a => a.derivedPowerOfAttorney).length;
  }

  // تصفية الوكالات
  filterAgencies(): void {
    this.filteredAgencies = this.agencies.filter(agency => {
      const matchesSearch = !this.searchTerm ||
        agency.agencyNumber.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        this.getClientName(agency.clientId).toLowerCase().includes(this.searchTerm.toLowerCase());

      const matchesType = !this.selectedType || agency.agencyType === this.selectedType;

      return matchesSearch && matchesType;
    });
    this.totalItems = this.filteredAgencies.length;
    this.pageIndex = 1;
  }

  // فتح نموذج الإضافة
  openAddModal(): void {
    this.newAgency = {
      agencyNumber: this.generateAgencyNumber(),
      issueDate: new Date(),
      expiryDate: undefined,
      issuingAuthority: '',
      clientId: 0,
      officeID: this.officeId,
      lawyerID: undefined,
      agencyType: 'عامة',
      derivedPowerOfAttorney: false
    };
    this.selectedRepresentative = 'office';
    this.canBeDerived = true;
    this.isAddModalVisible = true;
  }

  // توليد رقم وكالة تلقائي
  private generateAgencyNumber(): string {
    const count = this.agencies.length + 1;
    return `AG-${new Date().getFullYear()}-${count.toString().padStart(3, '0')}`;
  }

  // إضافة وكالة جديدة
  addAgency(): void {
    if (this.isFormValid(this.newAgency)) {
      // التأكد من اختيار الممثل (المكتب أو المحامي)
      if (!this.selectedRepresentative) {
        this.message.error('يرجى اختيار الممثل (المكتب أو المحامي)');
        return;
      }

      // إذا كانت الوكالة مشتقة، يجب أن تكون الممثل هو المكتب
      if (this.newAgency.derivedPowerOfAttorney && this.selectedRepresentative !== 'office') {
        this.message.error('الوكالات المشتقة يجب أن يكون الممثل فيها هو المكتب فقط');
        return;
      }

      const newAgency: Agency = {
        ...this.newAgency,
        id: this.agencies.length + 1,
        createdAt: new Date()
      };

      // تعيين الممثل المختار فقط
      if (this.selectedRepresentative === 'office') {
        newAgency.officeID = this.officeId;
        newAgency.lawyerID = undefined;
      } else if (this.selectedRepresentative === 'lawyer') {
        newAgency.lawyerID = this.newAgency.lawyerID;
        newAgency.officeID = undefined;
      }

      this.agencies.push(newAgency);
      this.filteredAgencies = [...this.agencies];
      this.calculateStats();
      this.isAddModalVisible = false;
      this.message.success('تم إضافة الوكالة بنجاح');
      this.resetForm();
    } else {
      this.message.error('يرجى ملء جميع الحقول المطلوبة');
    }
  }

  // فتح نموذج التعديل
  openEditModal(agency: Agency): void {
    this.editedAgency = { ...agency };

    // تحديد الممثل الحالي
    if (agency.officeID) {
      this.selectedRepresentative = 'office';
      this.canBeDerived = true;
    } else if (agency.lawyerID) {
      this.selectedRepresentative = 'lawyer';
      this.canBeDerived = false;
    } else {
      this.selectedRepresentative = null;
      this.canBeDerived = false;
    }

    this.isEditModalVisible = true;
  }

  // تحديث الوكالة
  updateAgency(): void {
    if (this.isFormValid(this.editedAgency)) {
      // التأكد من اختيار الممثل (المكتب أو المحامي)
      if (!this.selectedRepresentative) {
        this.message.error('يرجى اختيار الممثل (المكتب أو المحامي)');
        return;
      }

      // إذا كانت الوكالة مشتقة، يجب أن تكون الممثل هو المكتب
      if (this.editedAgency.derivedPowerOfAttorney && this.selectedRepresentative !== 'office') {
        this.message.error('الوكالات المشتقة يجب أن يكون الممثل فيها هو المكتب فقط');
        return;
      }

      const index = this.agencies.findIndex(a => a.id === this.editedAgency.id);
      if (index !== -1) {
        // تعيين الممثل المختار فقط
        if (this.selectedRepresentative === 'office') {
          this.editedAgency.officeID = this.officeId;
          this.editedAgency.lawyerID = undefined;
        } else if (this.selectedRepresentative === 'lawyer') {
          this.editedAgency.lawyerID = this.editedAgency.lawyerID;
          this.editedAgency.officeID = undefined;
        }

        this.agencies[index] = {
          ...this.editedAgency,
          updatedAt: new Date()
        };
        this.filteredAgencies = [...this.agencies];
        this.isEditModalVisible = false;
        this.message.success('تم تحديث بيانات الوكالة بنجاح');
      }
    } else {
      this.message.error('يرجى ملء جميع الحقول المطلوبة');
    }
  }

  // حذف الوكالة
  deleteAgency(agency: Agency): void {
    this.modal.confirm({
      nzTitle: 'تأكيد الحذف',
      nzContent: `هل أنت متأكد من حذف الوكالة "${agency.agencyNumber}"؟`,
      nzOkText: 'نعم، احذف',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzCancelText: 'إلغاء',
      nzOnOk: () => {
        const index = this.agencies.findIndex(a => a.id === agency.id);
        if (index !== -1) {
          this.agencies.splice(index, 1);
          this.filteredAgencies = [...this.agencies];
          this.calculateStats();
          this.message.success('تم حذف الوكالة بنجاح');
        }
      }
    });
  }

  // التحقق من صحة النموذج
  private isFormValid(agency: Agency): boolean {
    const basicValid = !!agency.agencyNumber &&
           !!agency.issueDate &&
           !!agency.issuingAuthority &&
           agency.clientId > 0;

    if (!basicValid) return false;

    // التحقق من اختيار الممثل
    if (!this.selectedRepresentative) {
      return false;
    }

    // إذا كان الممثل محامي، يجب اختيار محامي
    if (this.selectedRepresentative === 'lawyer' && !agency.lawyerID) {
      return false;
    }

    // إذا كانت الوكالة مشتقة، يجب أن يكون الممثل هو المكتب
    if (agency.derivedPowerOfAttorney && this.selectedRepresentative !== 'office') {
      return false;
    }

    return true;
  }

  // إعادة تعيين النموذج
  private resetForm(): void {
    this.newAgency = {
      agencyNumber: this.generateAgencyNumber(),
      issueDate: new Date(),
      expiryDate: undefined,
      issuingAuthority: '',
      clientId: 0,
      officeID: this.officeId,
      lawyerID: undefined,
      agencyType: 'عامة',
      derivedPowerOfAttorney: false
    };
    this.selectedRepresentative = 'office';
    this.canBeDerived = true;
  }

  // معالجة اختيار الممثل
  onRepresentativeChange(type: 'office' | 'lawyer' | null): void {
    this.selectedRepresentative = type;

    if (type === 'office') {
      this.newAgency.officeID = this.officeId;
      this.newAgency.lawyerID = undefined;
      // عند اختيار المكتب، يمكن جعل الوكالة قابلة للاشتقاق
      this.canBeDerived = true;
    } else if (type === 'lawyer') {
      // تعيين محامي افتراضي إذا لم يكن محدد
      this.newAgency.lawyerID = this.newAgency.lawyerID || this.lawyers[0]?.id;
      this.newAgency.officeID = undefined;
      // عند اختيار محامي، لا يمكن جعل الوكالة قابلة للاشتقاق
      this.canBeDerived = false;
      this.newAgency.derivedPowerOfAttorney = false;
    } else {
      this.newAgency.officeID = undefined;
      this.newAgency.lawyerID = undefined;
      this.canBeDerived = false;
      this.newAgency.derivedPowerOfAttorney = false;
    }
  }

  // معالجة تحميل الملفات
  handleUploadChange(info: NzUploadChangeParam, isEdit: boolean = false): void {
    if (info.file.status === 'done') {
      this.message.success(`${info.file.name} تم رفع الملف بنجاح`);

      if (isEdit) {
        this.editedAgency.documentFile = info.file.name;
      } else {
        this.newAgency.documentFile = info.file.name;
      }
    } else if (info.file.status === 'error') {
      this.message.error(`${info.file.name} فشل في رفع الملف`);
    }
  }

  // دالة beforeUpload
  beforeUpload = (file: NzUploadFile): boolean => {
    const isValidType = this.checkFileType(file);
    const isValidSize = this.checkFileSize(file);

    if (!isValidType) {
      this.message.error('نوع الملف غير مدعوم. المسموح: PDF, JPG, PNG');
      return false;
    }

    if (!isValidSize) {
      this.message.error('حجم الملف كبير جداً. الحد الأقصى 10MB');
      return false;
    }

    return true;
  }

  // التحقق من نوع الملف
  private checkFileType(file: NzUploadFile): boolean {
    const allowedTypes = ['application/pdf', 'image/jpeg', 'image/png', 'image/jpg'];

    if (file.type) {
      return allowedTypes.includes(file.type);
    }

    const fileName = file.name?.toLowerCase() || '';
    return fileName.endsWith('.pdf') ||
           fileName.endsWith('.jpg') ||
           fileName.endsWith('.jpeg') ||
           fileName.endsWith('.png');
  }

  // التحقق من حجم الملف
  private checkFileSize(file: NzUploadFile): boolean {
    const maxSize = 10 * 1024 * 1024; // 10MB
    return file.size ? file.size <= maxSize : true;
  }

  // الحصول على اسم العميل
  getClientName(clientId: number): string {
    const client = this.clients.find(c => c.id === clientId);
    return client?.fullName || 'غير معروف';
  }

  // الحصول على اسم المحامي
  getLawyerName(lawyerId?: number): string {
    if (!lawyerId) return '---';
    const lawyer = this.lawyers.find(l => l.id === lawyerId);
    return lawyer?.fullName || 'غير معروف';
  }

  // التحقق من انتهاء الصلاحية
  isExpired(expiryDate?: Date): boolean {
    if (!expiryDate) return false;
    return new Date(expiryDate) <= new Date();
  }

  // تنسيق التاريخ
  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString('ar-SA');
  }

  // الحصول على تسمية الإحصائيات
  getStatLabel(key: string): string {
    const labels: { [key: string]: string } = {
      'total': 'إجمالي الوكالات',
      'active': 'وكالات نشطة',
      'expired': 'وكالات منتهية',
      'derived': 'وكالات قابلة للاشتقاق'
    };
    return labels[key] || key;
  }

  // الحصول على أيقونة الإحصائيات
  getStatIcon(key: string): string {
    const icons: { [key: string]: string } = {
      'total': 'file-protect',
      'active': 'check-circle',
      'expired': 'clock-circle',
      'derived': 'swap'
    };
    return icons[key] || 'pie-chart';
  }

  // الحصول على البيانات للصفحة الحالية
  get paginatedAgencies(): Agency[] {
    const startIndex = (this.pageIndex - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.filteredAgencies.slice(startIndex, endIndex);
  }

  // تحديث الباجينيشن
  onPageChange(pageIndex: number): void {
    this.pageIndex = pageIndex;
  }

  onPageSizeChange(pageSize: number): void {
    this.pageSize = pageSize;
    this.pageIndex = 1;
  }
}
