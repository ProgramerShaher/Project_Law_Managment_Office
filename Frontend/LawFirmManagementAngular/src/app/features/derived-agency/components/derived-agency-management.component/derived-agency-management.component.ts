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
interface DerivedAgency {
  id?: number;
  parentPowerOfAttorneyId: number;
  lawyerId: number;
  derivedNumber: string;
  issueDate: Date;
  expiryDate?: Date;
  authorityScope?: string;
  isActive: boolean;
  notes?: string;
  documentFile?: string;
  createdAt?: Date;
  updatedAt?: Date;
}

interface ParentAgency {
  id: number;
  agencyNumber: string;
  clientName: string;
  issueDate: Date;
  expiryDate?: Date;
}

interface Lawyer {
  id: number;
  fullName: string;
  type: string;
}

@Component({
  selector: 'app-derived-agency-management',
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
  templateUrl: './derived-agency-management.component.html',
  styleUrls: ['./derived-agency-management.component.scss']
})
export class DerivedAgencyManagementComponent implements OnInit {
  // البيانات
  derivedAgencies: DerivedAgency[] = [];
  filteredDerivedAgencies: DerivedAgency[] = [];

  // القوائم المنسدلة
  parentAgencies: ParentAgency[] = [];
  lawyers: Lawyer[] = [];

  // النماذج
  newDerivedAgency: DerivedAgency = {
    parentPowerOfAttorneyId: 0,
    lawyerId: 0,
    derivedNumber: '',
    issueDate: new Date(),
    expiryDate: undefined,
    authorityScope: '',
    isActive: true,
    notes: ''
  };

  editedDerivedAgency: DerivedAgency = { ...this.newDerivedAgency };

  // حالة التطبيق
  searchTerm: string = '';
  selectedStatus: string = '';
  isAddModalVisible = false;
  isEditModalVisible = false;
  loading = false;

  // الباجينيشن
  pageSize = 10;
  pageIndex = 1;
  totalItems = 0;

  // الإحصائيات
  stats = {
    total: 0,
    active: 0,
    expired: 0,
    inactive: 0
  };

  constructor(
    private modal: NzModalService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.loadSampleData();
    this.calculateStats();
    this.filteredDerivedAgencies = [...this.derivedAgencies];
    this.totalItems = this.derivedAgencies.length;
  }

  // تحميل بيانات نموذجية
  private loadSampleData(): void {
    // تحميل الوكالات الأصلية
    this.parentAgencies = [
      {
        id: 1,
        agencyNumber: 'AG-2024-001',
        clientName: 'محمد أحمد العلي',
        issueDate: new Date('2024-01-15'),
        expiryDate: new Date('2025-01-15')
      },
      {
        id: 2,
        agencyNumber: 'AG-2024-002',
        clientName: 'فاطمة خالد السعد',
        issueDate: new Date('2024-02-20'),
        expiryDate: new Date('2024-08-20')
      },
      {
        id: 3,
        agencyNumber: 'AG-2024-003',
        clientName: 'خالد سعد القحطاني',
        issueDate: new Date('2024-03-10'),
        expiryDate: new Date('2024-09-10')
      }
    ];

    // تحميل المحامين
    this.lawyers = [
      { id: 1, fullName: 'أحمد محمد العلي', type: 'شريك' },
      { id: 2, fullName: 'فاطمة عبدالله السعد', type: 'محامي أول' },
      { id: 3, fullName: 'خالد سعد القحطاني', type: 'محامي مبتدئ' },
      { id: 4, fullName: 'نورة راشد الحربي', type: 'محامي أول' }
    ];

    // تحميل الوكالات المشتقة
    this.derivedAgencies = [
      {
        id: 1,
        parentPowerOfAttorneyId: 1,
        lawyerId: 1,
        derivedNumber: 'DER-2024-001',
        issueDate: new Date('2024-02-01'),
        expiryDate: new Date('2024-12-01'),
        authorityScope: 'تمثيل العميل في القضايا المدنية والتجارية',
        isActive: true,
        notes: 'وكالة مشتقة للتعامل مع القضايا المستعجلة',
        documentFile: 'derived_agency_1.pdf',
        createdAt: new Date('2024-02-01')
      },
      {
        id: 2,
        parentPowerOfAttorneyId: 2,
        lawyerId: 2,
        derivedNumber: 'DER-2024-002',
        issueDate: new Date('2024-03-15'),
        expiryDate: new Date('2024-09-15'),
        authorityScope: 'تمثيل العميل في المحاكم الجزائية',
        isActive: true,
        notes: 'وكالة خاصة بالقضايا الجزائية فقط',
        documentFile: 'derived_agency_2.pdf',
        createdAt: new Date('2024-03-15')
      },
      {
        id: 3,
        parentPowerOfAttorneyId: 1,
        lawyerId: 3,
        derivedNumber: 'DER-2024-003',
        issueDate: new Date('2024-04-01'),
        expiryDate: new Date('2024-10-01'),
        authorityScope: 'إجراءات التوثيق والمستندات',
        isActive: false,
        notes: 'موقفة مؤقتاً',
        documentFile: 'derived_agency_3.pdf',
        createdAt: new Date('2024-04-01')
      }
    ];
  }

  // حساب الإحصائيات
  private calculateStats(): void {
    this.stats.total = this.derivedAgencies.length;
    this.stats.active = this.derivedAgencies.filter(a =>
      a.isActive && (!a.expiryDate || new Date(a.expiryDate) > new Date())
    ).length;
    this.stats.expired = this.derivedAgencies.filter(a =>
      a.expiryDate && new Date(a.expiryDate) <= new Date()
    ).length;
    this.stats.inactive = this.derivedAgencies.filter(a => !a.isActive).length;
  }

  // تصفية الوكالات المشتقة
  filterDerivedAgencies(): void {
    this.filteredDerivedAgencies = this.derivedAgencies.filter(agency => {
      const matchesSearch = !this.searchTerm ||
        agency.derivedNumber.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        this.getParentAgencyInfo(agency.parentPowerOfAttorneyId).toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        this.getLawyerName(agency.lawyerId).toLowerCase().includes(this.searchTerm.toLowerCase());

      const matchesStatus = !this.selectedStatus ||
        (this.selectedStatus === 'active' && agency.isActive && (!agency.expiryDate || new Date(agency.expiryDate) > new Date())) ||
        (this.selectedStatus === 'expired' && agency.expiryDate && new Date(agency.expiryDate) <= new Date()) ||
        (this.selectedStatus === 'inactive' && !agency.isActive);

      return matchesSearch && matchesStatus;
    });
    this.totalItems = this.filteredDerivedAgencies.length;
    this.pageIndex = 1;
  }

  // فتح نموذج الإضافة
  openAddModal(): void {
    this.newDerivedAgency = {
      parentPowerOfAttorneyId: 0,
      lawyerId: 0,
      derivedNumber: this.generateDerivedNumber(),
      issueDate: new Date(),
      expiryDate: undefined,
      authorityScope: '',
      isActive: true,
      notes: ''
    };
    this.isAddModalVisible = true;
  }

  // توليد رقم وكالة مشتقة تلقائي
  private generateDerivedNumber(): string {
    const count = this.derivedAgencies.length + 1;
    return `DER-${new Date().getFullYear()}-${count.toString().padStart(3, '0')}`;
  }

  // إضافة وكالة مشتقة جديدة
  addDerivedAgency(): void {
    if (this.isFormValid(this.newDerivedAgency)) {
      const newDerivedAgency: DerivedAgency = {
        ...this.newDerivedAgency,
        id: this.derivedAgencies.length + 1,
        createdAt: new Date()
      };

      this.derivedAgencies.push(newDerivedAgency);
      this.filteredDerivedAgencies = [...this.derivedAgencies];
      this.calculateStats();
      this.isAddModalVisible = false;
      this.message.success('تم إضافة الوكالة المشتقة بنجاح');
      this.resetForm();
    } else {
      this.message.error('يرجى ملء جميع الحقول المطلوبة');
    }
  }

  // فتح نموذج التعديل
  openEditModal(agency: DerivedAgency): void {
    this.editedDerivedAgency = { ...agency };
    this.isEditModalVisible = true;
  }

  // تحديث الوكالة المشتقة
  updateDerivedAgency(): void {
    if (this.isFormValid(this.editedDerivedAgency)) {
      const index = this.derivedAgencies.findIndex(a => a.id === this.editedDerivedAgency.id);
      if (index !== -1) {
        this.derivedAgencies[index] = {
          ...this.editedDerivedAgency,
          updatedAt: new Date()
        };
        this.filteredDerivedAgencies = [...this.derivedAgencies];
        this.isEditModalVisible = false;
        this.message.success('تم تحديث بيانات الوكالة المشتقة بنجاح');
      }
    } else {
      this.message.error('يرجى ملء جميع الحقول المطلوبة');
    }
  }

  // حذف الوكالة المشتقة
  deleteDerivedAgency(agency: DerivedAgency): void {
    this.modal.confirm({
      nzTitle: 'تأكيد الحذف',
      nzContent: `هل أنت متأكد من حذف الوكالة المشتقة "${agency.derivedNumber}"؟`,
      nzOkText: 'نعم، احذف',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzCancelText: 'إلغاء',
      nzOnOk: () => {
        const index = this.derivedAgencies.findIndex(a => a.id === agency.id);
        if (index !== -1) {
          this.derivedAgencies.splice(index, 1);
          this.filteredDerivedAgencies = [...this.derivedAgencies];
          this.calculateStats();
          this.message.success('تم حذف الوكالة المشتقة بنجاح');
        }
      }
    });
  }

  // تبديل حالة النشاط
  toggleAgencyStatus(agency: DerivedAgency): void {
    agency.isActive = !agency.isActive;
    agency.updatedAt = new Date();
    this.calculateStats();
    this.message.success(`تم ${agency.isActive ? 'تفعيل' : 'إيقاف'} الوكالة المشتقة بنجاح`);
  }

  // التحقق من صحة النموذج
  private isFormValid(agency: DerivedAgency): boolean {
    return !!agency.parentPowerOfAttorneyId &&
           !!agency.lawyerId &&
           !!agency.derivedNumber &&
           !!agency.issueDate;
  }

  // إعادة تعيين النموذج
  private resetForm(): void {
    this.newDerivedAgency = {
      parentPowerOfAttorneyId: 0,
      lawyerId: 0,
      derivedNumber: this.generateDerivedNumber(),
      issueDate: new Date(),
      expiryDate: undefined,
      authorityScope: '',
      isActive: true,
      notes: ''
    };
  }

  // معالجة تحميل الملفات
  handleUploadChange(info: NzUploadChangeParam, isEdit: boolean = false): void {
    if (info.file.status === 'done') {
      this.message.success(`${info.file.name} تم رفع الملف بنجاح`);

      if (isEdit) {
        this.editedDerivedAgency.documentFile = info.file.name;
      } else {
        this.newDerivedAgency.documentFile = info.file.name;
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

  // الحصول على معلومات الوكالة الأصلية
  getParentAgencyInfo(parentId: number): string {
    const parent = this.parentAgencies.find(p => p.id === parentId);
    return parent ? `${parent.agencyNumber} - ${parent.clientName}` : 'غير معروف';
  }

  // الحصول على اسم المحامي
  getLawyerName(lawyerId: number): string {
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
      'total': 'إجمالي الوكالات المشتقة',
      'active': 'وكالات نشطة',
      'expired': 'وكالات منتهية',
      'inactive': 'وكالات موقفة'
    };
    return labels[key] || key;
  }

  // الحصول على أيقونة الإحصائيات
  getStatIcon(key: string): string {
    const icons: { [key: string]: string } = {
      'total': 'file-protect',
      'active': 'check-circle',
      'expired': 'clock-circle',
      'inactive': 'pause-circle'
    };
    return icons[key] || 'pie-chart';
  }

  // الحصول على البيانات للصفحة الحالية
  get paginatedDerivedAgencies(): DerivedAgency[] {
    const startIndex = (this.pageIndex - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.filteredDerivedAgencies.slice(startIndex, endIndex);
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
