import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { VentanaComponent } from './component/ventana/ventana.component';


const routeConfig: Routes = [
  {
    path: '',
    redirectTo: 'principal',
    pathMatch: 'full'
  },
  {
    path: 'principal',
    component: VentanaComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];


export default routeConfig;
