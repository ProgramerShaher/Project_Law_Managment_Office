import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NzCardModule } from 'ng-zorro-antd/card';
import { NzStatisticModule } from 'ng-zorro-antd/statistic';
import { NzIconModule } from 'ng-zorro-antd/icon';
import { NzGridModule } from 'ng-zorro-antd/grid';
import { NzToolTipModule } from 'ng-zorro-antd/tooltip';

interface LawyerStats {
  total: number;
  trainee: number;
  consultant: number;
  expert: number;
  other: number;
}

@Component({
  selector: 'app-lawyer-stats',
  standalone: true,
  imports: [
    CommonModule,
    NzCardModule,
    NzStatisticModule,
    NzIconModule,
    NzGridModule,
    NzToolTipModule
  ],
  template: `
    <div nz-row nzGutter="16" class="stats-container">
      <!-- إجمالي المحامين -->
      <div nz-col nzXs="24" nzSm="12" nzMd="6">
        <nz-card class="stat-card total-card" nzHoverable>
          <div class="card-content">
            <div class="icon-wrapper total">
              <span nz-icon nzType="team" nzTheme="fill"></span>
            </div>
            <div class="stat-content">
              <div class="value">{{ stats.total }}</div>
              <div class="label">إجمالي المحامين</div>
              <div class="trend positive">+12% هذا الشهر</div>
            </div>
          </div>
        </nz-card>
      </div>

      <!-- المحامون المتدربون -->
      <div nz-col nzXs="24" nzSm="12" nzMd="6">
        <nz-card class="stat-card trainee-card" nzHoverable>
          <div class="card-content">
            <div class="icon-wrapper trainee">
              <span nz-icon nzType="user-add" nzTheme="outline"></span>
            </div>
            <div class="stat-content">
              <div class="value">{{ stats.trainee }}</div>
              <div class="label">محامون متدربون</div>
              <div class="percentage">{{ getPercentage('trainee') }} من الإجمالي</div>
            </div>
          </div>
        </nz-card>
      </div>

      <!-- المحامون المستشارون -->
      <div nz-col nzXs="24" nzSm="12" nzMd="6">
        <nz-card class="stat-card consultant-card" nzHoverable>
          <div class="card-content">
            <div class="icon-wrapper consultant">
              <span nz-icon nzType="solution" nzTheme="outline"></span>
            </div>
            <div class="stat-content">
              <div class="value">{{ stats.consultant }}</div>
              <div class="label">محامون مستشارون</div>
              <div class="percentage">{{ getPercentage('consultant') }} من الإجمالي</div>
            </div>
          </div>
        </nz-card>
      </div>

      <!-- المحامون الخبراء -->
      <div nz-col nzXs="24" nzSm="12" nzMd="6">
        <nz-card class="stat-card expert-card" nzHoverable>
          <div class="card-content">
            <div class="icon-wrapper expert">
              <span nz-icon nzType="crown" nzTheme="outline"></span>
            </div>
            <div class="stat-content">
              <div class="value">{{ stats.expert }}</div>
              <div class="label">محامون خبراء</div>
              <div class="percentage">{{ getPercentage('expert') }} من الإجمالي</div>
            </div>
          </div>
        </nz-card>
      </div>
    </div>
  `,
  styles: [`
    .stats-container {
      margin-bottom: 24px;
    }

    .stat-card {
      border-radius: 16px;
      border: none;
      box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
      transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
      overflow: hidden;
      position: relative;

      &::before {
        content: '';
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        height: 4px;
        background: linear-gradient(90deg, var(--gradient-start), var(--gradient-end));
        opacity: 0;
        transition: opacity 0.3s ease;
      }

      &:hover::before {
        opacity: 1;
      }

      &:hover {
        transform: translateY(-8px);
        box-shadow: 0 12px 40px rgba(0, 0, 0, 0.15);
      }
    }

    .total-card {
      --gradient-start: #1890ff;
      --gradient-end: #36cfc9;
    }

    .trainee-card {
      --gradient-start: #52c41a;
      --gradient-end: #b7eb8f;
    }

    .consultant-card {
      --gradient-start: #fa8c16;
      --gradient-end: #ffc53d;
    }

    .expert-card {
      --gradient-start: #eb2f96;
      --gradient-end: #f759ab;
    }

    .card-content {
      display: flex;
      align-items: center;
      padding: 8px 0;
    }

    .icon-wrapper {
      width: 64px;
      height: 64px;
      border-radius: 16px;
      display: flex;
      align-items: center;
      justify-content: center;
      margin-left: 16px;
      flex-shrink: 0;

      span[nz-icon] {
        font-size: 28px;
      }

      &.total {
        background: linear-gradient(135deg, #e6f7ff, #bae7ff);
        color: #1890ff;
      }

      &.trainee {
        background: linear-gradient(135deg, #f6ffed, #d9f7be);
        color: #52c41a;
      }

      &.consultant {
        background: linear-gradient(135deg, #fff7e6, #ffd591);
        color: #fa8c16;
      }

      &.expert {
        background: linear-gradient(135deg, #fff0f6, #ffadd2);
        color: #eb2f96;
      }
    }

    .stat-content {
      flex: 1;
    }

    .value {
      font-size: 32px;
      font-weight: 700;
      line-height: 1;
      color: #262626;
      margin-bottom: 4px;
    }

    .label {
      font-size: 14px;
      color: #8c8c8c;
      font-weight: 500;
      margin-bottom: 4px;
    }

    .percentage, .trend {
      font-size: 12px;
      font-weight: 500;
    }

    .percentage {
      color: #595959;
    }

    .trend.positive {
      color: #52c41a;
    }

    .trend.negative {
      color: #ff4d4f;
    }

    /* تصميم متجاوب */
    @media (max-width: 768px) {
      .card-content {
        padding: 16px 0;
      }

      .icon-wrapper {
        width: 56px;
        height: 56px;
        margin-left: 12px;

        span[nz-icon] {
          font-size: 24px;
        }
      }

      .value {
        font-size: 28px;
      }
    }

    @media (max-width: 576px) {
      .stat-card {
        margin-bottom: 16px;
      }

      .icon-wrapper {
        width: 48px;
        height: 48px;

        span[nz-icon] {
          font-size: 20px;
        }
      }

      .value {
        font-size: 24px;
      }

      .label {
        font-size: 13px;
      }
    }
  `]
})
export class LawyerStatsComponent {
  @Input() stats: LawyerStats = {
    total: 0,
    trainee: 0,
    consultant: 0,
    expert: 0,
    other: 0
  };

  getPercentage(key: keyof LawyerStats): string {
    if (this.stats.total === 0) return '0%';
    const percentage = (this.stats[key] / this.stats.total) * 100;
    return `${percentage.toFixed(1)}%`;
  }
}
