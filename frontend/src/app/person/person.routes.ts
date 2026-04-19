import { Routes } from '@angular/router';

export const PERSON_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () => import('./pages/person-list/person-list').then(m => m.PersonList)
  },
  {
    path: 'create',
    loadComponent: () => import('./pages/person-form/person-form').then(m => m.PersonForm)
  },
  {
    path: 'edit/:id',
    loadComponent: () => import('./pages/person-form/person-form').then(m => m.PersonForm)
  }
];