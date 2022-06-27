import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PagerComponent } from './components/pager/pager.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { FormsModule } from '@angular/forms';
import { PageHeaderComponent } from './components/page-header/page-header.component';

@NgModule({
  declarations: [
    PagerComponent,
    PageHeaderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    PaginationModule.forRoot()
  ],
  exports: [
    PagerComponent,
    PageHeaderComponent
  ]
})
export class SharedModule { }
