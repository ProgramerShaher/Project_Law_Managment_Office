import { Component, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzSelectModule } from 'ng-zorro-antd/select';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzGridModule } from 'ng-zorro-antd/grid';

@Component({
  selector: 'app-lawyer-search',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    NzCardModule,
    NzInputModule,
    NzSelectModule,
    NzButtonModule,
    NzIconModule,
    NzGridModule
  ],
  template: `
    <nz-card class="search-card" nzHoverable>
      <div class="card-header">
        <div class="header-content">
          <span nz-icon nzType="filter" class="header-icon"></span>
          <h3 class="header-title">بحث وتصفية المحامين</h3>
        </div>
        <div class="header-actions">
          <button
            nz-button
            nzType="text"
            nzSize="small"
            class="clear-btn"
            (click)="clearFilters()">
            مسح الكل
          </button>
        </div>
      </div>

      <div nz-row nzGutter="20" nzAlign="middle" nzJustify="space-between" class="search-fields">
        <!-- حقل البحث -->
        <div nz-col nzXs="24" nzSm="24" nzMd="10" nzLg="8">
          <div class="search-field">
            <label class="field-label">بحث بالاسم</label>
            <nz-input-group
              nzPrefixIcon="search"
              nzSize="large"
              class="search-input">
              <input
                nz-input
                placeholder="ابحث باسم المحامي أو البريد الإلكتروني..."
                [(ngModel)]="searchTerm"
                (input)="onSearchChange()"
                class="custom-input"
              />
            </nz-input-group>
          </div>
        </div>

        <!-- تصفية النوع -->
        <div nz-col nzXs="24" nzSm="24" nzMd="8" nzLg="6">
          <div class="search-field">
            <label class="field-label">نوع المحامي</label>
            <nz-select
              nzPlaceHolder="اختر نوع المحامي"
              nzSize="large"
              [(ngModel)]="selectedType"
              (ngModelChange)="onFilterChange()"
              class="type-select">
              <nz-option nzValue="" nzLabel="جميع المحامين"></nz-option>
              <nz-option nzValue="1" nzLabel="👨‍🎓 متدرب"></nz-option>
              <nz-option nzValue="2" nzLabel="💼 مستشار"></nz-option>
              <nz-option nzValue="3" nzLabel="⭐ خبير"></nz-option>
              <nz-option nzValue="4" nzLabel="🔷 أخرى"></nz-option>
            </nz-select>
          </div>
        </div>

        <!-- زر التطبيق -->
        <div nz-col nzXs="24" nzSm="24" nzMd="6" nzLg="4">
          <div class="search-field action-field">
            <label class="field-label invisible-label">إجراء</label>
            <button
              nz-button
              nzType="primary"
              nzSize="large"
              class="apply-btn full-width"
              (click)="onFilterApply()"
              [nzLoading]="loading">
              <span nz-icon nzType="search" nzTheme="outline"></span>
              بحث
            </button>
          </div>
        </div>
      </div>

      <!-- فلترات إضافية -->
      <div class="additional-filters" *ngIf="showAdvancedFilters">
        <div nz-row nzGutter="16" nzAlign="middle">
          <div nz-col nzXs="24" nzSm="12" nzMd="8">
            <div class="search-field">
              <label class="field-label">حالة المحامي</label>
              <nz-select
                nzPlaceHolder="الحالة"
                nzSize="default"
                [(ngModel)]="selectedStatus"
                class="status-select">
                <nz-option nzValue="" nzLabel="جميع الحالات"></nz-option>
                <nz-option nzValue="active" nzLabel="🟢 نشط"></nz-option>
                <nz-option nzValue="inactive" nzLabel="⚪ غير نشط"></nz-option>
                <nz-option nzValue="pending" nzLabel="🟡 قيد المراجعة"></nz-option>
              </nz-select>
            </div>
          </div>
        </div>
      </div>

      <!-- تبديل الفلترات المتقدمة -->
      <div class="advanced-toggle">
        <button
          nz-button
          nzType="link"
          nzSize="small"
          (click)="toggleAdvancedFilters()"
          class="toggle-btn">
          <span nz-icon [nzType]="showAdvancedFilters ? 'up' : 'down'"></span>
          {{ showAdvancedFilters ? 'إخفاء الفلترات المتقدمة' : 'إظهار فلترات إضافية' }}
        </button>
      </div>
    </nz-card>
  `,
  styles: [`
    .search-card {
      border-radius: 16px;
      border: none;
      box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
      margin-bottom: 24px;
      overflow: hidden;
    }

    .card-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      padding: 16px 24px;
      border-bottom: 1px solid #f0f0f0;
      background: linear-gradient(135deg, #f8f9fa 0%, #e9ecef 100%);
    }

    .header-content {
      display: flex;
      align-items: center;
      gap: 12px;
    }

    .header-icon {
      font-size: 20px;
      color: #1890ff;
    }

    .header-title {
      margin: 0;
      font-size: 18px;
      font-weight: 600;
      color: #262626;
    }

    .clear-btn {
      color: #8c8c8c;
      font-size: 14px;
    }

    .search-fields {
      padding: 24px;
    }

    .search-field {
      margin-bottom: 0;
    }

    .field-label {
      display: block;
      margin-bottom: 8px;
      font-weight: 500;
      color: #595959;
      font-size: 14px;
    }

    .invisible-label {
      opacity: 0;
    }

    .search-input {
      border-radius: 12px;
    }

    .custom-input {
      border-radius: 12px;
    }

    .type-select, .status-select {
      width: 100%;
      border-radius: 12px;
    }

    .apply-btn {
      border-radius: 12px;
      font-weight: 500;
      height: 40px;
      box-shadow: 0 2px 8px rgba(24, 144, 255, 0.3);
      transition: all 0.3s ease;

      &:hover {
        transform: translateY(-1px);
        box-shadow: 0 4px 12px rgba(24, 144, 255, 0.4);
      }
    }

    .full-width {
      width: 100%;
    }

    .additional-filters {
      padding: 0 24px 24px;
      border-top: 1px solid #f0f0f0;
      margin-top: 16px;
      padding-top: 24px;
      animation: slideDown 0.3s ease;
    }

    .advanced-toggle {
      padding: 0 24px 16px;
      text-align: center;
    }

    .toggle-btn {
      color: #1890ff;
      font-size: 14px;
    }

    .action-field {
      display: flex;
      flex-direction: column;
      justify-content: flex-end;
      height: 100%;
    }

    /* تأثيرات الحركة */
    @keyframes slideDown {
      from {
        opacity: 0;
        transform: translateY(-10px);
      }
      to {
        opacity: 1;
        transform: translateY(0);
      }
    }

    /* تحسينات التصميم المتجاوب */
    @media (max-width: 768px) {
      .search-fields {
        padding: 16px;
      }

      .card-header {
        padding: 12px 16px;
      }

      .header-title {
        font-size: 16px;
      }

      .additional-filters {
        padding: 0 16px 16px;
      }
    }

    @media (max-width: 576px) {
      .search-card {
        border-radius: 12px;
      }

      .search-fields {
        padding: 12px;
      }

      .header-content {
        gap: 8px;
      }

      .header-icon {
        font-size: 18px;
      }

      .header-title {
        font-size: 15px;
      }
    }

    /* تحسينات إضافية */
    ::ng-deep .ant-input-group .ant-input-affix-wrapper {
      border-radius: 12px;
    }

    ::ng-deep .ant-select-selector {
      border-radius: 12px !important;
    }

    ::ng-deep .ant-btn {
      border-radius: 12px;
    }
  `]
})
export class LawyerSearchComponent {
  searchTerm: string = '';
  selectedType: string = '';
  selectedStatus: string = '';
  showAdvancedFilters: boolean = false;
  loading: boolean = false;

  @Output() searchChange = new EventEmitter<string>();
  @Output() filterChange = new EventEmitter<string>();
  @Output() filterApply = new EventEmitter<void>();

  onSearchChange(): void {
    this.searchChange.emit(this.searchTerm);
  }

  onFilterChange(): void {
    this.filterChange.emit(this.selectedType);
  }

  onFilterApply(): void {
    this.loading = true;
    this.filterApply.emit();

    // محاكاة التحميل (يمكن إزالته في التطبيق الحقيقي)
    setTimeout(() => {
      this.loading = false;
    }, 1000);
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.selectedType = '';
    this.selectedStatus = '';
    this.showAdvancedFilters = false;
    this.onSearchChange();
    this.onFilterChange();
  }

  toggleAdvancedFilters(): void {
    this.showAdvancedFilters = !this.showAdvancedFilters;
  }
}
