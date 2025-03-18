/// <reference types="@angular/localize" />

import { bootstrapApplication } from '@angular/platform-browser';
import { importProvidersFrom } from '@angular/core'; // ✅ Adicionando corretamente
import { appConfig } from './app/app.config';
import { AppComponent } from './app/app.component';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';


const updatedAppConfig = {
  ...appConfig,
  providers: [
    ...(appConfig.providers || []),
    importProvidersFrom(ToastModule),
    MessageService
  ]
};

// ✅ Inicializando a aplicação com a configuração corrigida
bootstrapApplication(AppComponent, updatedAppConfig)
  .catch(err => console.error(err));
