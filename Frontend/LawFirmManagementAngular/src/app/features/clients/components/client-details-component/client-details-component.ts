// components/client-details/client-details.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Client, ClientRole } from '../../services/clients.service';
import { NzTagComponent } from "ng-zorro-antd/tag";
import { NzDescriptionsComponent, NzDescriptionsItemComponent } from "ng-zorro-antd/descriptions";
import { NzDividerComponent } from "ng-zorro-antd/divider";

@Component({
  selector: 'app-client-details',
  template: `
    <div nz-row *ngIf="client">
      <div nz-col nzSpan="8" class="text-center">
        <div class="client-avatar-large">
          <i nz-icon nzType="user"></i>
        </div>
        <h4 class="mt-3">{{client.fullName}}</h4>
        <nz-tag [nzColor]="getClientTypeClass(client.clientType)">
          {{getClientTypeLabel(client.clientType)}}
        </nz-tag>
      </div>
      <div nz-col nzSpan="16">
        <nz-descriptions [nzColumn]="2" nzSize="small">
          <nz-descriptions-item nzTitle="رقم العميل">
            {{client.id || '---'}}
          </nz-descriptions-item>
          <nz-descriptions-item nzTitle="تاريخ الميلاد">
            {{formatDate(client.birthDate)}}
          </nz-descriptions-item>
          <nz-descriptions-item nzTitle="نوع العميل">
            {{getClientTypeLabel(client.clientType)}}
          </nz-descriptions-item>
          <nz-descriptions-item nzTitle="دور العميل">
            {{getClientRoleName(client.clientRoleId)}}
          </nz-descriptions-item>
          <nz-descriptions-item nzTitle="رقم الهاتف">
            {{client.phoneNumber}}
          </nz-descriptions-item>
          <nz-descriptions-item nzTitle="البريد الإلكتروني">
            {{client.email || '---'}}
          </nz-descriptions-item>
          <nz-descriptions-item nzTitle="العنوان" [nzSpan]="2">
            {{client.address || '---'}}
          </nz-descriptions-item>
        </nz-descriptions>

        <div *ngIf="client.urlImageNationalId" class="mt-4 text-center">
          <nz-divider></nz-divider>
          <h5>صورة الهوية الوطنية</h5>
          <img [src]="client.urlImageNationalId" alt="صورة الهوية"
               class="details-image" />
        </div>
      </div>
    </div>
  `,
  imports: [NzTagComponent, NzDescriptionsComponent, NzDescriptionsItemComponent, NzDividerComponent]
})
export class ClientDetailsComponent {
  @Input() client!: Client;
  @Input() clientRoles: ClientRole[] = [];

  getClientTypeLabel(type: number): string {
    const labels: { [key: number]: string } = {
      1: 'فردي',
      2: 'شركة',
      3: 'شخص'
    };
    return labels[type] || 'غير محدد';
  }

  getClientTypeClass(type: number): string {
    const classes: { [key: number]: string } = {
      1: 'blue',
      2: 'green',
      3: 'orange'
    };
    return classes[type] || 'blue';
  }

  getClientRoleName(roleId: number): string {
    const role = this.clientRoles.find(r => r.id === roleId);
    return role ? role.name : 'غير محدد';
  }

  formatDate(date: string | Date): string {
    if (!date) return '---';
    return new Date(date).toLocaleDateString('ar-SA');
  }
}
