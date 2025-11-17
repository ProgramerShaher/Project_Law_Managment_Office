import { DerivedAgencyManagementComponent } from './features/derived-agency/components/derived-agency-management.component/derived-agency-management.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './layout/layout';

// إنشاء مكون بسيط للوحة التحكم
import { Component } from '@angular/core';
import { DashboardComponent } from './features/dashboard/dashboard';
import { ClientComponent } from './features/clients/components/client/client';
import { LawyerComponent } from './features/lawyers/components/lawyer/lawyer';
import { AgencyManagementComponent } from './features/agencys/components/agency-management.component/agency-management.component';



const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'clients', component:ClientComponent },
        { path: 'lawyers', component: LawyerComponent },
       { path: 'agencies/direct', component: AgencyManagementComponent },
       { path: 'agencies/derived', component: DerivedAgencyManagementComponent },
    //   { path: 'reports', component: DashboardComponent },
    //   { path: 'settings', component: DashboardComponent }
     ]
  }
];

// تصدير المسارات مباشرة
export const appRoutes = routes;
