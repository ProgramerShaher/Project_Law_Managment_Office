import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
// import { Lawyer } from '../services/lawyer.service';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzPaginationModule } from 'ng-zorro-antd/pagination';
import { NzEmptyModule } from 'ng-zorro-antd/empty';
import { NzSpinModule } from 'ng-zorro-antd/spin';
import { LawyerTypePipe } from '../../../../shared/pipes/lawyer-type.pipe';
import { LawyerTypeColorPipe } from '../../../../shared/pipes/lawyer-type-color.pipe';
import { AgeCalculatorPipe } from '../../../../shared/pipes/age-calculator.pipe';
import { Lawyer } from '../../services/lawyer.service';
// import { LawyerTypePipe } from '../pipes/lawyer-type.pipe';
// import { LawyerTypeColorPipe } from '../pipes/lawyer-type-color.pipe';
// import { AgeCalculatorPipe } from '../pipes/age-calculator.pipe';

@Component({
  selector: 'app-lawyer-table',
  standalone: true,
  imports: [
    CommonModule,
    NzTableModule,
    NzCardModule,
    NzTagModule,
    NzButtonModule,
    NzAvatarModule,
    NzToolTipModule,
    NzIconModule,
    NzPaginationModule,
    NzEmptyModule,
    NzSpinModule,
    LawyerTypePipe,
    LawyerTypeColorPipe,
    AgeCalculatorPipe
  ],
  template: `
    <nz-card class="table-card">
      <div class="table-container">
        <nz-table
          #lawyersTable
          nzShowSizeChanger
          [nzData]="paginatedLawyers"
          [nzLoading]="loading"
          [nzTotal]="totalItems"
          [nzPageSize]="pageSize"
          [nzPageIndex]="pageIndex"
          (nzPageIndexChange)="onPageChange($event)"
          (nzPageSizeChange)="onPageSizeChange($event)"
          nzTableLayout="fixed"
          class="custom-table">
          <thead>
            <tr>
              <th nzWidth="60px">#</th>
              <th>الاسم الكامل</th>
              <th nzWidth="150px">رقم الهاتف</th>
              <th>البريد الإلكتروني</th>
              <th nzWidth="100px">العمر</th>
              <th nzWidth="120px">النوع</th>
              <th nzWidth="150px">الإجراءات</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let lawyer of lawyersTable.data; let i = index">
              <td>{{ (pageIndex - 1) * pageSize + i + 1 }}</td>
              <td>
                <div class="lawyer-info">
                  <nz-avatar
                    nzSize="small"
                    [nzSrc]="lawyer.identityImagePath || 'https://placehold.co/40x40'"
                    class="lawyer-avatar">
                  </nz-avatar>
                  <div class="lawyer-details">
                    <strong>{{ lawyer.fullName }}</strong>
                    <div class="text-muted">{{ lawyer.address }}</div>
                  </div>
                </div>
              </td>
              <td>{{ lawyer.phoneNumber }}</td>
              <td>{{ lawyer.email || '---' }}</td>
              <td>
                <nz-tag *ngIf="lawyer.dateOfBirth" [nzColor]="'blue'">
                  {{ lawyer.dateOfBirth | ageCalculator }} سنة
                </nz-tag>
                <span *ngIf="!lawyer.dateOfBirth" class="text-muted">---</span>
              </td>
              <td>
                <nz-tag [nzColor]="lawyer.type | lawyerTypeColor">
                  {{ lawyer.type | lawyerType }}
                </nz-tag>
              </td>
              <td>
                <div class="action-buttons">
                  <button
                    nz-button
                    nzType="default"
                    nzSize="small"
                    nz-tooltip="عرض التفاصيل"
                    class="action-btn view"
                    (click)="onViewDetails(lawyer)">
                    <span nz-icon nzType="eye" nzTheme="outline"></span>
                  </button>
                  <button
                    nz-button
                    nzType="default"
                    nzSize="small"
                    nz-tooltip="تعديل"
                    class="action-btn edit"
                    (click)="onEdit(lawyer)">
                    <span nz-icon nzType="edit" nzTheme="outline"></span>
                  </button>
                  <button
                    nz-button
                    nzType="default"
                    nzSize="small"
                    nz-tooltip="حذف"
                    class="action-btn delete"
                    (click)="onDelete(lawyer)">
                    <span nz-icon nzType="delete" nzTheme="outline"></span>
                  </button>
                </div>
              </td>
            </tr>
          </tbody>
        </nz-table>

        <!-- Empty State -->
        <div *ngIf="filteredLawyers.length === 0 && !loading" class="empty-state">
          <nz-empty nzNotFoundImage="simple">
            <span nz-typography nzType="secondary">لا توجد نتائج للعرض</span>
          </nz-empty>
        </div>
      </div>
    </nz-card>
  `,
  styleUrls: ['./lawyer-table.component.scss']
})
export class LawyerTableComponent {
  @Input() lawyers: Lawyer[] = [];
  @Input() filteredLawyers: Lawyer[] = [];
  @Input() loading: boolean = false;
  @Input() pageSize: number = 10;
  @Input() pageIndex: number = 1;
  @Input() totalItems: number = 0;

  @Output() pageChange = new EventEmitter<number>();
  @Output() pageSizeChange = new EventEmitter<number>();
  @Output() viewDetails = new EventEmitter<Lawyer>();
  @Output() edit = new EventEmitter<Lawyer>();
  @Output() delete = new EventEmitter<Lawyer>();

  get paginatedLawyers(): Lawyer[] {
    const startIndex = (this.pageIndex - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;
    return this.filteredLawyers.slice(startIndex, endIndex);
  }

  onPageChange(pageIndex: number): void {
    this.pageChange.emit(pageIndex);
  }

  onPageSizeChange(pageSize: number): void {
    this.pageSizeChange.emit(pageSize);
  }

  onViewDetails(lawyer: Lawyer): void {
    this.viewDetails.emit(lawyer);
  }

  onEdit(lawyer: Lawyer): void {
    this.edit.emit(lawyer);
  }

  onDelete(lawyer: Lawyer): void {
    this.delete.emit(lawyer);
  }
}
