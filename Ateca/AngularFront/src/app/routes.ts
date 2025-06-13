import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { HomeComponent } from './pages/home/home.component';


const routeConfig: Routes = [
  {
    path: '',
    component: LoginComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: 'registro',
    component: RegisterComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: 'principal',
    component: HomeComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

export default routeConfig;
