import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzModalModule, NzModalService } from 'ng-zorro-antd/modal';
import { NzMessageService } from 'ng-zorro-antd/message';
import { NzIconModule } from 'ng-zorro-antd/icon';

import { Lawyer, LawyerService, LawyerType } from '../../services/lawyer.service';
import { LawyerStatsComponent } from '../lawyer-stats.component/lawyer-stats.component';
import { LawyerSearchComponent } from '../lawyer-search.component/lawyer-search.component';
import { LawyerTableComponent } from '../lawyer-table.component/lawyer-table.component';
import { LawyerFormComponent } from '../lawyer-form.component/lawyer-form.component';

@Component({
  selector: 'app-lawyer-management',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    NzButtonModule,
    NzModalModule,
    NzIconModule,
    LawyerStatsComponent,
    LawyerSearchComponent,
    LawyerTableComponent,
    LawyerFormComponent
  ],
  templateUrl: './lawyer.html',
  styleUrls: ['./lawyer.scss']
})
export class LawyerComponent implements OnInit {
  lawyers: Lawyer[] = [];
  filteredLawyers: Lawyer[] = [];

  // إضافة المتغيرات المفقودة
  searchTerm: string = '';
  selectedType: string = '';

  // حالة التطبيق
  isAddModalVisible = false;
  isEditModalVisible = false;
  loading = false;
  selectedLawyer: Lawyer | null = null;

  // التقسيم
  pageSize = 10;
  pageIndex = 1;
  totalItems = 0;

  // الإحصائيات
  stats = {
    total: 0,
    trainee: 0,
    consultant: 0,
    expert: 0,
    other: 0
  };

  constructor(
    private lawyerService: LawyerService,
    private modal: NzModalService,
    private message: NzMessageService
  ) {}

  ngOnInit(): void {
    this.loadLawyers();
  }

  // تحميل البيانات من API
  private loadLawyers(): void {
    this.loading = true;
    this.lawyerService.getAllLawyers().subscribe({
      next: (data) => {
        this.lawyers = data;
        this.filteredLawyers = [...this.lawyers];
        this.calculateStats();
        this.totalItems = this.lawyers.length;
        this.loading = false;
      },
      error: (error) => {
        this.message.error('فشل في تحميل بيانات المحامين');
        console.error('Error loading lawyers:', error);
        this.loading = false;
      }
    });
  }

  // حساب الإحصائيات
  private calculateStats(): void {
    this.stats.total = this.lawyers.length;
    this.stats.trainee = this.lawyers.filter(l => l.type === LawyerType.Trainee).length;
    this.stats.consultant = this.lawyers.filter(l => l.type === LawyerType.Consultant).length;
    this.stats.expert = this.lawyers.filter(l => l.type === LawyerType.Expert).length;
    this.stats.other = this.lawyers.filter(l => l.type === LawyerType.Other).length;
  }

  // البحث والتصفية
  onSearchChange(searchTerm: string): void {
    this.searchTerm = searchTerm;
    this.filterLawyers();
  }

  onFilterChange(selectedType: string): void {
    this.selectedType = selectedType;
    this.filterLawyers();
  }

  onFilterApply(): void {
    this.filterLawyers();
  }

  filterLawyers(): void {
    this.filteredLawyers = this.lawyers.filter(lawyer => {
      const matchesSearch = !this.searchTerm ||
        lawyer.fullName.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        lawyer.email?.toLowerCase().includes(this.searchTerm.toLowerCase()) ||
        lawyer.phoneNumber.includes(this.searchTerm);

      const matchesType = !this.selectedType || lawyer.type.toString() === this.selectedType;

      return matchesSearch && matchesType;
    });
    this.totalItems = this.filteredLawyers.length;
    this.pageIndex = 1;
  }

  // فتح نموذج الإضافة
  openAddModal(): void {
    this.isAddModalVisible = true;
  }

  // فتح نموذج التعديل
  openEditModal(lawyer: Lawyer): void {
    this.selectedLawyer = lawyer;
    this.isEditModalVisible = true;
  }

  // إضافة محامي جديد
  onLawyerAdded(lawyer: Lawyer): void {
    this.lawyers.push(lawyer);
    this.filteredLawyers = [...this.lawyers];
    this.calculateStats();
    this.isAddModalVisible = false;
    this.message.success('تم إضافة المحامي بنجاح');
  }

  
  // تحديث المحامي
  onLawyerUpdated(updatedLawyer: Lawyer): void {
    const index = this.lawyers.findIndex(l => l.id === updatedLawyer.id);
    if (index !== -1) {
      this.lawyers[index] = updatedLawyer;
      this.filteredLawyers = [...this.lawyers];
      this.isEditModalVisible = false;
      this.selectedLawyer = null;
      this.message.success('تم تحديث بيانات المحامي بنجاح');
    }
  }

  // حذف المحامي
  onDeleteLawyer(lawyer: Lawyer): void {
    if (!lawyer.id) return;

    this.modal.confirm({
      nzTitle: 'تأكيد الحذف',
      nzContent: `هل أنت متأكد من حذف المحامي "${lawyer.fullName}"؟`,
      nzOkText: 'نعم، احذف',
      nzOkType: 'primary',
      nzOkDanger: true,
      nzCancelText: 'إلغاء',
      nzOnOk: () => {
        this.loading = true;
        this.lawyerService.deleteLawyer(lawyer.id!).subscribe({
          next: () => {
            const index = this.lawyers.findIndex(l => l.id === lawyer.id);
            if (index !== -1) {
              this.lawyers.splice(index, 1);
              this.filteredLawyers = [...this.lawyers];
              this.calculateStats();
              this.loading = false;
              this.message.success('تم حذف المحامي بنجاح');
            }
          },
          error: (error) => {
            this.loading = false;
            this.message.error('فشل في حذف المحامي');
            console.error('Error deleting lawyer:', error);
          }
        });
      }
    });
  }

  // التقسيم
  onPageChange(pageIndex: number): void {
    this.pageIndex = pageIndex;
  }

  onPageSizeChange(pageSize: number): void {
    this.pageSize = pageSize;
    this.pageIndex = 1;
  }
}
