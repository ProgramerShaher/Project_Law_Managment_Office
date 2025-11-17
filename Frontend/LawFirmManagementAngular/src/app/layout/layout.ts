import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, RouterOutlet, Router } from '@angular/router';

// وحدات NG-ZORRO
import { NzLayoutModule } from 'ng-zorro-antd/layout';
import { NzMenuModule } from 'ng-zorro-antd/menu';
import { NzButtonModule } from 'ng-zorro-antd/button';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzDropDownModule } from 'ng-zorro-antd/dropdown';
import { NzAvatarModule } from 'ng-zorro-antd/avatar';
import { NzBreadCrumbModule } from 'ng-zorro-antd/breadcrumb';
import { NzInputModule } from 'ng-zorro-antd/input';
import { NzBadgeModule } from 'ng-zorro-antd/badge';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule,
    RouterOutlet,
    NzLayoutModule,
    NzMenuModule,
    NzButtonModule,
    NzIconModule,
    NzDropDownModule,
    NzAvatarModule,
    NzBreadCrumbModule,
    NzInputModule,
    NzBadgeModule
  ],
  templateUrl: './layout.html',
  styleUrls: ['./layout.scss']
})
export class LayoutComponent implements OnInit {
  isCollapsed = false;

  menuItems = [
    {
      title: 'لوحة التحكم',
      icon: 'dashboard',
      link: '/dashboard',
      isActive: true
    },
    {
      title: 'العملاء',
      icon: 'team',
      link: '/clients',
      isActive: false
    },
    {
      title: 'المحامين',
      icon: 'user',
      link: '/lawyers',
      isActive: false
    },
    {
  title: 'الوكالات',
  icon: 'file-protect',
  link: '/agencies',
  isActive: false,
  isOpen: false, // ← أضف هذا
  children: [
    { title: 'وكالات مباشرة', icon: 'file-done', link: '/agencies/direct', isActive: false },
    { title: 'وكالة مشتقة', icon: 'file-sync', link: '/agencies/derived', isActive: false }
  ]
},

    {
      title: 'القضايا',
      icon: 'file-text',
      link: '/cases',
      isActive: false
    },
    {
      title: 'الجلسات',
      icon: 'calendar',
      link: '/sessions',
      isActive: false
    },
    {
      title: 'المدفوعات',
      icon: 'dollar',
      link: '/payments',
      isActive: false
    },
    {
      title: 'التقارير',
      icon: 'bar-chart',
      link: '/reports',
      isActive: false
    },
    {
      title: 'الإعدادات',
      icon: 'setting',
      link: '/settings',
      isActive: false
    }
  ];

  userInfo = {
    name: 'أحمد المحامي',
    role: 'محامي رئيسي'
  };

  notifications = [
    { id: 1, title: 'جلسة جديدة', description: 'قضية أحمد محمد غداً', time: 'منذ 5 دقائق', read: false },
    { id: 2, title: 'دفعة مستحقة', description: 'قضية فاطمة علي', time: 'منذ ساعة', read: false },
    { id: 3, title: 'مستند جديد', description: 'تم رفع المستندات', time: 'منذ 3 ساعات', read: true }
  ];

  unreadNotifications = this.notifications.filter(n => !n.read).length;

  constructor(private router: Router) { }

  ngOnInit(): void { }

onMenuClick(clickedItem: any): void {
  if (clickedItem.children && clickedItem.children.length > 0) {
    // عكس حالة الفتح
    clickedItem.isOpen = !clickedItem.isOpen;
    return;
  }

  this.updateActiveState(clickedItem);
  this.router.navigate([clickedItem.link]);
}


  onSubMenuClick(parentItem: any, clickedSubItem: any): void {
    // تحديث الحالة النشطة للعناصر
    this.updateActiveState(clickedSubItem, parentItem);

    // التنقل إلى المسار المحدد
    this.router.navigate([clickedSubItem.link]);
  }

  private updateActiveState(clickedItem: any, parentItem?: any): void {
    // إعادة تعيين جميع العناصر
    this.menuItems.forEach(item => {
      item.isActive = false;
      if (item.children) {
        item.children.forEach(child => {
          child.isActive = false;
        });
      }
    });

    // تعيين العنصر النشط
    if (parentItem) {
      parentItem.isActive = true;
      clickedItem.isActive = true;
    } else {
      clickedItem.isActive = true;
    }
  }

  markAllAsRead(): void {
    this.notifications.forEach(n => n.read = true);
    this.unreadNotifications = 0;
  }

  onProfileClick(): void {
    console.log('الملف الشخصي');
  }

  onSettingsClick(): void {
    console.log('الإعدادات');
  }

  onPrivacyClick(): void {
    console.log('الخصوصية');
  }

  onLogoutClick(): void {
    console.log('تسجيل الخروج');
  }
}
