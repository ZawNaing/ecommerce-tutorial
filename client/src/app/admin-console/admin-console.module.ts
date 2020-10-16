import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminConsoleComponent } from './admin-console.component';
import { AdminRoutingModule } from './admin-routing.module';



@NgModule({
  declarations: [
    AdminConsoleComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule
  ]
})
export class AdminConsoleModule { }
