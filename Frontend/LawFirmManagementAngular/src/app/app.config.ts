import { ApplicationConfig, importProvidersFrom, inject, LOCALE_ID } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';
import { provideAnimations } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


// ng-zorro providers
import { provideNzIcons } from 'ng-zorro-antd/icon';
import { IconDefinition } from '@ant-design/icons-angular';
import * as AllIcons from '@ant-design/icons-angular/icons';
import en from '@angular/common/locales/en';
import ar from '@angular/common/locales/ar';
import { en_US, NZ_I18N,ar_EG, fr_FR } from 'ng-zorro-antd/i18n';
import { registerLocaleData } from '@angular/common';

registerLocaleData(en);
registerLocaleData(ar);
// استيراد المسارات من ملف routes
import { appRoutes } from './app.routes';

const antDesignIcons = AllIcons as {
  [key: string]: IconDefinition;
};
const icons: IconDefinition[] = Object.keys(antDesignIcons).map(key => antDesignIcons[key]);

export const appConfig: ApplicationConfig = {
 providers: [
    {

      provide: NZ_I18N,
      useFactory: () => {
        const localId = inject(LOCALE_ID);
        switch (localId) {
          case 'ar':
            return ar_EG;
          /** keep the same with angular.json/i18n/locales configuration **/
          case 'fr':
            return ar_EG;
          default:
            return ar_EG;
        }
      }
    },    provideRouter(appRoutes),
    provideHttpClient(),
    provideAnimations(),
    provideNzIcons(icons),
    provideNzIcons(icons),
    importProvidersFrom(
      FormsModule,
      ReactiveFormsModule

    ),

  ]
};
