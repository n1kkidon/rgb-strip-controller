import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginFormComponent } from './login-form/login-form.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
import { HomepageComponent } from './homepage/homepage.component';

const routes: Routes = [

  {
    path: 'login',
    component: LoginFormComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: 'GUEST',
      },
    },
  },
  {
    path: 'home',
    component: HomepageComponent,
    canActivate: [NgxPermissionsGuard],
    data: {
      permissions: {
        only: ['GUEST', 'ROLE_USER', 'ROLE_ADMIN'],
      },
    },
    children: [
      { path: '', component: HomepageComponent, pathMatch: 'full' },
    ],
  },
  { path: '**', redirectTo: 'home' },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {




}
