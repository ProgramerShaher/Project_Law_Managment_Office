// components/client-table/client-table.component.ts
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Client, ClientRole } from '../../services/clients.service';
import { NzCardComponent } from "ng-zorro-antd/card";
import { SharedModule } from '../../../../shared/shared-module';
import { NzSpaceComponent } from "ng-zorro-antd/space";
import { NzOptionComponent } from "ng-zorro-antd/select";
import { NzTableComponent } from "ng-zorro-antd/table";
import { NzEmptyComponent } from "ng-zorro-antd/empty";
import { NzTagComponent } from "ng-zorro-antd/tag";
@Component({
  selector: 'app-client-table',
  templateUrl: './client-table-component.html',
    imports: [SharedModule, NzCardComponent, NzSpaceComponent, NzOptionComponent, NzTableComponent, NzEmptyComponent, NzTagComponent]
})
export class ClientTableComponent {
  @Input() clients: Client[] = [];
  @Input() paginatedClients: Client[] = [];
  @Input() clientRoles: ClientRole[] = [];
  @Input() isLoading: boolean = false;
  @Input() currentPage: number = 1;
  @Input() pageSize: number = 10;
  @Input() sortField: string = 'fullName';
  @Input() sortOrder: 'ascend' | 'descend' = 'ascend';

  @Output() onPageChange = new EventEmitter<number>();
  @Output() onPageSizeChange = new EventEmitter<number>();
  @Output() onSortFieldChange = new EventEmitter<string>();
  @Output() onToggleSort = new EventEmitter<void>();
  @Output() onViewDetails = new EventEmitter<Client>();
  @Output() onEdit = new EventEmitter<Client>();
  @Output() onDelete = new EventEmitter<Client>();
  @Output() onAddClient = new EventEmitter<void>();

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

  isNewClient(client: Client): boolean {
    const clientDate = new Date(client.birthDate);
    const currentDate = new Date();
    return clientDate.getMonth() === currentDate.getMonth() &&
           clientDate.getFullYear() === currentDate.getFullYear();
  }
}
