import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddComponent } from './add/add.component';
import { ListComponent } from './list/list.component';
import { EditComponent } from './edit/edit.component';
import { ButtonModule, CardModule, FormModule, SpinnerModule, TableModule } from '@coreui/angular';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CategoryRoutingModule } from './category-routing.module';

@NgModule({
  declarations: [
    AddComponent,
    ListComponent,
    EditComponent,
  ],
  imports: [
    CommonModule,
    CardModule,
    TableModule,
    HttpClientModule,
    ButtonModule,
    FormModule,
    ReactiveFormsModule,
    SpinnerModule,
    CategoryRoutingModule
  ]
})
export class CategoryModule { }
