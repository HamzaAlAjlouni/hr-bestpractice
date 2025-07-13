import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ErrorPageComponent } from '../error-page/error-page.component';
import { RouterModule, Routes } from '@angular/router';

const router: Routes = [

  { path: '**', component: ErrorPageComponent }
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(router, { useHash: true }),
    CommonModule
  ]
})
// tslint:disable-next-line:class-name
export class HR_RouterModule { }
