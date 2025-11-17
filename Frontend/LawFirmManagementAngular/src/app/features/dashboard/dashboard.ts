import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

// وحدات NG-ZORRO
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzTableModule } from 'ng-zorro-antd/table';
import { NzTagModule } from 'ng-zorro-antd/tag';
import { NzListModule } from 'ng-zorro-antd/list';
import { NzTypographyModule } from 'ng-zorro-antd/typography';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    NzCardModule,
    NzGridModule,
    NzIconModule,
    NzTableModule,
    NzTagModule,
    NzListModule,
    NzTypographyModule,
    NzAvatarModule
  ],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.scss']
})
export class DashboardComponent implements OnInit {
  stats = [
    {
      title: 'إجمالي العملاء',
      value: 24,
      icon: 'team',
      color: '#1890ff',
      change: '+12%'
    },
    {
      title: 'القضايا النشطة',
      value: 18,
      icon: 'file-text',
      color: '#52c41a',
      change: '+8%'
    },
    {
      title: 'الجلسات القادمة',
      value: 7,
      icon: 'calendar',
      color: '#faad14',
      change: '+3%'
    },
    {
      title: 'إجمالي الإيرادات',
      value: '45,000',
      icon: 'dollar',
      color: '#f5222d',
      change: '+15%'
    }
  ];

  recentCases = [
    { id: 1, client: 'أحمد محمد', type: 'تجاري', status: 'نشط', date: '2024-01-15' },
    { id: 2, client: 'فاطمة علي', type: 'جنائي', status: 'معلق', date: '2024-01-14' },
    { id: 3, client: 'خالد عبدالله', type: 'مدني', status: 'نشط', date: '2024-01-13' },
    { id: 4, client: 'سارة أحمد', type: 'أحوال شخصية', status: 'منتهي', date: '2024-01-12' }
  ];

  upcomingSessions = [
    { case: 'قضية تجارية - أحمد محمد', date: '2024-01-20', time: '10:00 ص', court: 'محكمة التجارة' },
    { case: 'قضية جنائية - فاطمة علي', date: '2024-01-22', time: '11:30 ص', court: 'المحكمة الجزائية' },
    { case: 'قضية مدنية - خالد عبدالله', date: '2024-01-25', time: '09:00 ص', court: 'محكمة البداية' }
  ];

  constructor() { }

  ngOnInit(): void { }

  getStatusColor(status: string): string {
    switch (status) {
      case 'نشط': return 'green';
      case 'معلق': return 'orange';
      case 'منتهي': return 'red';
      default: return 'default';
    }
  }
}
