import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListComponent } from './list/list.component';
import { AddComponent } from './add/add.component';
import { EditComponent } from './edit/edit.component';

const routes: Routes = [
  {
    path: 'list',
    component: ListComponent,
    data: {
      title: 'Card List'
    }
  },
  {
    path: 'add',
    component: AddComponent,
    data: {
      title: 'Card Add'
    }
  },
  {
    path: 'edit/:id',
    component: EditComponent,
    data: {
      title: 'Card Edit'
    }
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CardsRoutingModule {
}
