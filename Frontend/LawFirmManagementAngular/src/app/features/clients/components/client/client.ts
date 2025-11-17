import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  ClientsService,
  Client,
  ClientRole,
  CreateClientRequest,
} from '../../services/clients.service';

// NG-ZORRO Modules
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { NzFormModule } from 'ng-zorro-antd/form';
import { NzUploadModule, NzUploadFile } from 'ng-zorro-antd/upload';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzPopconfirmModule } from 'ng-zorro-antd/popconfirm';
import { NzDescriptionsModule } from 'ng-zorro-antd/descriptions';
import { NzDividerModule } from 'ng-zorro-antd/divider';
import { NzSpaceModule } from 'ng-zorro-antd/space';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzAlertModule } from 'ng-zorro-antd/alert';
import { NzDatePickerModule } from 'ng-zorro-antd/date-picker';
import { UploadPictureComponent } from '../../../../shared/file-upload/file-upload.component';

// الأنماط المتوافقة مع الـ API
enum ClientType {
  Individual = 1,
  Company = 2,
  Person = 3,
}

@Component({
  selector: 'app-client',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    // NG-ZORRO Modules
    UploadPictureComponent,
    NzDatePickerModule,
    NzTableModule,
    NzButtonModule,
    NzInputModule,
    NzSelectModule,
    NzModalModule,
    NzCardModule,
    NzIconModule,
    NzTagModule,
    NzPaginationModule,
    NzSpinModule,
    NzFormModule,
    NzUploadModule,
    NzGridModule,
    NzStatisticModule,
    NzToolTipModule,
    NzPopconfirmModule,
    NzDescriptionsModule,
    NzDividerModule,
    NzSpaceModule,
    NzEmptyModule,
    NzAlertModule,
  ],
  templateUrl: './client.html',
  styleUrls: ['./client.scss'],
})
export class ClientComponent implements OnInit {
  // البيانات
  clients: Client[] = [];
  filteredClients: Client[] = [];
  clientRoles: ClientRole[] = [];

  // النماذج
  newClient: Client = {
    fullName: '',
    birthDate: '',
    clientType: ClientType.Individual,
    clientRoleId: 0,
    phoneNumber: '',
    email: '',
    address: '',
    urlImageNationalId: '',
  };

  editedClient: Client = { ...this.newClient };
  newRoleName: string = '';

  // حالة التطبيق
  searchTerm: string = '';
  selectedType: string = '';
  selectedRole: string = '';
  isAddModalVisible = false;
  isEditModalVisible = false;
  isDetailsModalVisible = false;
  isAddRoleModalVisible = false;
  isLoading = false;
  isSubmitting = false;
  isRoleSubmitting = false;
  selectedClient: Client | null = null;

  // التصفية والترتيب
  sortField = 'fullName';
  sortOrder: 'ascend' | 'descend' = 'ascend';
  currentPage = 1;
  pageSize = 10;

  // خصائص جديدة لرفع الملفات
  urlImageNationalId: string = '';
  isEditMode: boolean = false;
  showValidationErrors: boolean = false;

  // الإحصائيات
  stats = {
    total: 0,
    newThisMonth: 0,
    active: 0,
    inactive: 0,
  };

  constructor(
    private clientsService: ClientsService,
    private modal: NzModalService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.loadClients();
    this.loadClientRoles();
  }

  onIdentityImageChanged(fileUrl: string): void {
    console.log('📸 حدث تغيير في صورة الهوية:', fileUrl);
    console.log('🔄 الحالة الحالية لـ urlImageNationalId قبل التحديث:', this.urlImageNationalId);
    this.urlImageNationalId = fileUrl;
    this.newClient.urlImageNationalId = fileUrl;

    if (fileUrl) {
      console.log('✅ تم تعيين urlImageNationalId بنجاح:', this.urlImageNationalId);
      this.message.success('تم رفع صورة الهوية بنجاح');
      this.showValidationErrors = false;
    } else {
      console.log('❌ تم مسح صورة الهوية');
      this.urlImageNationalId = '';
    }
  }

  // تحميل البيانات الحقيقية من الـ API
  private loadClients(): void {
    this.isLoading = true;

    this.clientsService.getClients().subscribe({
      next: (data) => {
        this.clients = data;
        this.filteredClients = [...this.clients];
        this.calculateStats();
        this.sortClients();
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading clients:', error);
        this.message.error('حدث خطأ في تحميل بيانات العملاء');
        this.isLoading = false;
      },
    });
  }

  // تحميل أدوار العملاء
  private loadClientRoles(): void {
    this.clientsService.getClientRoles().subscribe({
      next: (roles) => {
        this.clientRoles = roles;
        // تعيين دور افتراضي إذا كان هناك أدوار
        if (roles.length > 0 && this.newClient.clientRoleId === 0) {
          this.newClient.clientRoleId = roles[0].id;
        }
      },
      error: (error) => {
        console.error('Error loading client roles:', error);
        this.message.error('حدث خطأ في تحميل أدوار العملاء');
      },
    });
  }

  // حساب الإحصائيات
  private calculateStats(): void {
    this.stats.total = this.clients.length;

    const currentMonth = new Date().getMonth();
    const currentYear = new Date().getFullYear();

    this.stats.newThisMonth = this.clients.filter((client) => {
      const clientDate = new Date(client.birthDate);
      return clientDate.getMonth() === currentMonth && clientDate.getFullYear() === currentYear;
    }).length;

    this.stats.active = this.clients.length;
    this.stats.inactive = 0;
  }

  // تصفية العملاء
  filterClients(): void {
    this.filteredClients = this.clients.filter((client) => {
      const matchesSearch =
        !this.searchTerm ||
        client.fullName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        client.email?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        client.phoneNumber.includes(this.searchTerm);

      const matchesType =
        !this.selectedType || this.getClientTypeLabel(client.clientType) === this.selectedType;

      const matchesRole =
        !this.selectedRole || this.getClientRoleName(client.clientRoleId) === this.selectedRole;

      return matchesSearch && matchesType && matchesRole;
    });

    this.sortClients();
    this.currentPage = 1;
  }

  // ترتيب العملاء
  sortClients(): void {
    this.filteredClients.sort((a, b) => {
      let aValue: any = a[this.sortField as keyof Client];
      let bValue: any = b[this.sortField as keyof Client];

      if (this.sortField === 'birthDate') {
        aValue = new Date(aValue);
        bValue = new Date(bValue);
      }

      if (aValue < bValue) {
        return this.sortOrder === 'ascend' ? -1 : 1;
      }
      if (aValue > bValue) {
        return this.sortOrder === 'ascend' ? 1 : -1;
      }
      return 0;
    });
  }

  // تبديل اتجاه الترتيب
  toggleSortOrder(): void {
    this.sortOrder = this.sortOrder === 'ascend' ? 'descend' : 'ascend';
    this.sortClients();
  }

  // مسح الفلاتر
  clearFilters(): void {
    this.searchTerm = '';
    this.selectedType = '';
    this.selectedRole = '';
    this.filterClients();
  }

  // فتح نموذج الإضافة
  openAddModal(): void {
    this.newClient = {
      fullName: '',
      birthDate: new Date().toISOString().split('T')[0],
      clientType: ClientType.Individual,
      clientRoleId: this.clientRoles.length > 0 ? this.clientRoles[0].id : 0,
      phoneNumber: '',
      urlImageNationalId: '',
      email: '',
      address: '',
    };
    this.urlImageNationalId = '';
    this.isAddModalVisible = true;
    this.isEditMode = false;
    this.showValidationErrors = false;
  }

  // إغلاق نموذج الإضافة
  closeAddModal(): void {
    this.isAddModalVisible = false;
    this.resetForm();
  }

  // إضافة عميل جديد
  addClient(): void {
    this.isSubmitting = true;
    this.showValidationErrors = true;

    // التحقق من الصورة في وضع الإضافة
    if (!this.urlImageNationalId) {
      this.message.error('يرجى رفع صورة الهوية الوطنية');
      this.isSubmitting = false;
      return;
    }

    if (!this.isFormValid(this.newClient)) {
      this.message.error('يرجى ملء جميع الحقول المطلوبة');
      this.isSubmitting = false;
      return;
    }

    // تحضير بيانات الطلب مع التأكد من إرسال مسار الصورة
    const clientRequest: CreateClientRequest = {
      fullName: this.newClient.fullName,
      birthDate: new Date(this.newClient.birthDate).toISOString(),
      clientType: this.newClient.clientType,
      clientRoleId: this.newClient.clientRoleId,
      phoneNumber: this.newClient.phoneNumber,
      email: this.newClient.email || undefined,
      address: this.newClient.address || undefined,
      urlImageNationalId: this.newClient.urlImageNationalId,
    };

    console.log('🔄 بيانات العميل المرسلة:', JSON.stringify(clientRequest, null, 2));

    this.clientsService.createClient(clientRequest).subscribe({
      next: (client) => {
        console.log('✅ استجابة الـ API بعد الإضافة:', client);
        this.message.success('تم إضافة العميل بنجاح');
        this.loadClients();
        this.isAddModalVisible = false;
        this.resetForm();
        this.isSubmitting = false;
        this.urlImageNationalId = '';
        this.showValidationErrors = false;
      },
      error: (error) => {
        console.error('❌ Error creating client:', error);
        if (error.status === 415) {
          this.message.error('خطأ في نوع البيانات المرسلة. يرجى التحقق من السيرفر');
        } else {
          this.message.error('حدث خطأ أثناء إضافة العميل');
        }
        this.isSubmitting = false;
      },
    });
  }

  // فتح نموذج التعديل
  openEditModal(client: Client): void {
    this.editedClient = { ...client };
    // تعيين صورة الهوية الحالية إذا كانت موجودة
    this.urlImageNationalId = (client.urlImageNationalId as string) || '';
    this.isEditMode = true;

    // تحويل التاريخ للتنسيق المناسب للـ input type="date"
    if (this.editedClient.birthDate) {
      const date = new Date(this.editedClient.birthDate);
      this.editedClient.birthDate = date.toISOString().split('T')[0];
    }
    this.isEditModalVisible = true;
  }

  // إغلاق نموذج التعديل
  closeEditModal(): void {
    this.isEditModalVisible = false;
    this.urlImageNationalId = '';
    this.isEditMode = false;
  }

  // تحديث العميل
  updateClient(): void {
    this.isSubmitting = true;

    if (!this.isFormValid(this.editedClient) || !this.editedClient.id) {
      this.message.error('يرجى ملء جميع الحقول المطلوبة');
      this.isSubmitting = false;
      return;
    }

    // استخدام الصورة الجديدة إذا تم رفعها، أو الاحتفاظ بالصورة القديمة
    const finalImageUrl =
      this.urlImageNationalId || (this.editedClient.urlImageNationalId as string);

    // تحضير بيانات الطلب
    const clientRequest: CreateClientRequest = {
      fullName: this.editedClient.fullName,
      birthDate: new Date(this.editedClient.birthDate).toISOString(),
      clientType: this.editedClient.clientType,
      clientRoleId: this.editedClient.clientRoleId,
      phoneNumber: this.editedClient.phoneNumber,
      email: this.editedClient.email || undefined,
      address: this.editedClient.address || undefined,
      urlImageNationalId: finalImageUrl || undefined, // تأكد من إرسال القيمة
    };

    console.log('🔄 بيانات التحديث المرسلة:', JSON.stringify(clientRequest, null, 2));

    this.clientsService.updateClient(this.editedClient.id, clientRequest).subscribe({
      next: (client) => {
        console.log('✅ استجابة الـ API بعد التحديث:', client);
        this.message.success('تم تحديث بيانات العميل بنجاح');
        this.loadClients();
        this.isEditModalVisible = false;
        this.isSubmitting = false;
        this.urlImageNationalId = '';
      },
      error: (error) => {
        console.error('❌ Error updating client:', error);
        if (error.status === 415) {
          this.message.error('خطأ في نوع البيانات المرسلة. يرجى التحقق من السيرفر');
        } else {
          this.message.error('حدث خطأ أثناء تحديث بيانات العميل');
        }
        this.isSubmitting = false;
      },
    });
  }

  // إضافة دور جديد
  addNewRole(): void {
    if (!this.newRoleName.trim()) {
      this.message.error('يرجى إدخال اسم الدور');
      return;
    }

    this.isRoleSubmitting = true;

    this.clientsService.createClientRole(this.newRoleName.trim()).subscribe({
      next: (newRole) => {
        this.clientRoles.push(newRole);
        this.newRoleName = '';
        this.isAddRoleModalVisible = false;
        this.isRoleSubmitting = false;
        this.message.success('تم إضافة الدور بنجاح');
      },
      error: (error) => {
        console.error('Error creating role:', error);
        this.message.error('حدث خطأ أثناء إضافة الدور');
        this.isRoleSubmitting = false;
      },
    });
  }

  // فتح نموذج إضافة الدور
  openAddRoleModal(): void {
    this.newRoleName = '';
    this.isAddRoleModalVisible = true;
  }

  // إغلاق نموذج إضافة الدور
  closeAddRoleModal(): void {
    this.isAddRoleModalVisible = false;
  }

  // عرض تفاصيل العميل
  viewClientDetails(client: Client): void {
    this.selectedClient = client;
    this.isDetailsModalVisible = true;
  }

  // إغلاق نموذج التفاصيل
  closeDetailsModal(): void {
    this.isDetailsModalVisible = false;
    this.selectedClient = null;
  }

  // تأكيد الحذف
  confirmDelete(client: Client): void {
    this.modal.confirm({
      nzTitle: 'تأكيد الحذف',
      nzContent: `هل أنت متأكد من حذف العميل <strong>${client.fullName}</strong>؟ هذا الإجراء لا يمكن التراجع عنه!`,
      nzOkText: 'نعم، احذف',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzOnOk: () => this.deleteClient(client),
      nzCancelText: 'إلغاء',
      nzCentered: true,
    });
  }

  // حذف العميل
  deleteClient(client: Client): void {
    if (!client.id) return;

    this.clientsService.deleteClient(client.id).subscribe({
      next: () => {
        this.message.success('تم حذف العميل بنجاح');
        this.loadClients();
      },
      error: (error) => {
        console.error('Error deleting client:', error);
        this.message.error('حدث خطأ أثناء حذف العميل');
      },
    });
  }

  // التحقق من صحة النموذج
  private isFormValid(client: Client): boolean {
    return (
      !!client.fullName &&
      !!client.birthDate &&
      !!client.phoneNumber &&
      !!client.clientType &&
      !!client.clientRoleId
    );
  }

  // إعادة تعيين النموذج
  private resetForm(): void {
    this.newClient = {
      fullName: '',
      birthDate: new Date().toISOString().split('T')[0],
      clientType: ClientType.Individual,
      clientRoleId: this.clientRoles.length > 0 ? this.clientRoles[0].id : 0,
      phoneNumber: '',
      email: '',
      address: '',
      urlImageNationalId: '',
    };
    this.urlImageNationalId = '';
    this.showValidationErrors = false;
  }

  // التصدير إلى Excel
  exportToExcel(): void {
    this.message.info('جاري تجهيز ملف Excel...');

    setTimeout(() => {
      this.message.success('تم تصدير البيانات بنجاح');
    }, 2000);
  }

  // التحقق إذا كان العميل جديد (أضيف هذا الشهر)
  isNewClient(client: Client): boolean {
    const clientDate = new Date(client.birthDate);
    const currentDate = new Date();
    return (
      clientDate.getMonth() === currentDate.getMonth() &&
      clientDate.getFullYear() === currentDate.getFullYear()
    );
  }

  // Pagination functions
  get totalPages(): number {
    return Math.ceil(this.filteredClients.length / this.pageSize);
  }

  get paginatedClients(): Client[] {
    const startIndex = (this.currentPage - 1) * this.pageSize;
    return this.filteredClients.slice(startIndex, startIndex + this.pageSize);
  }

  onPageChange(page: number): void {
    this.currentPage = page;
  }

  onPageSizeChange(size: number): void {
    this.pageSize = size;
    this.currentPage = 1;
  }

  // الحصول على تسمية نوع العميل
  getClientTypeLabel(type: number): string {
    const labels: { [key: number]: string } = {
      [ClientType.Individual]: 'فردي',
      [ClientType.Company]: 'شركة',
      [ClientType.Person]: 'شخص',
    };
    return labels[type] || 'غير محدد';
  }

  // الحصول على كلاس نوع العميل
  getClientTypeClass(type: number): string {
    const classes: { [key: number]: string } = {
      [ClientType.Individual]: 'blue',
      [ClientType.Company]: 'green',
      [ClientType.Person]: 'orange',
    };
    return classes[type] || 'blue';
  }

  // الحصول على اسم دور العميل
  getClientRoleName(roleId: number): string {
    const role = this.clientRoles.find((r) => r.id === roleId);
    return role ? role.name : 'غير محدد';
  }

  // تنسيق التاريخ
  formatDate(date: string | Date): string {
    if (!date) return '---';
    return new Date(date).toLocaleDateString('ar-SA');
  }

  // الحصول على تسمية الإحصائيات
  getStatLabel(key: string): string {
    const labels: { [key: string]: string } = {
      total: 'إجمالي العملاء',
      newThisMonth: 'العملاء الجدد',
      active: 'العملاء النشطين',
      inactive: 'العملاء غير النشطين',
    };
    return labels[key] || key;
  }

  // الحصول على أيقونة الإحصائيات
  getStatIcon(key: string): string {
    const icons: { [key: string]: string } = {
      total: 'user',
      newThisMonth: 'user-add',
      active: 'user-check',
      inactive: 'user-clock',
    };
    return icons[key] || 'bar-chart';
  }

  // الحصول على كلاس أيقونة الإحصائيات
  getStatIconClass(key: string): string {
    const classes: { [key: string]: string } = {
      total: 'primary',
      newThisMonth: 'success',
      active: 'info',
      inactive: 'warning',
    };
    return classes[key] || 'secondary';
  }
}
