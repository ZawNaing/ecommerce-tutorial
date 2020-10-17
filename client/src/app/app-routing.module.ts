import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AboutUsComponent } from './about-us/about-us.component';
import { ContactUsComponent } from './contact-us/contact-us.component';
import { AuthGuard } from './core/guards/auth.guard';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';
import { ServicesComponent } from './services/services.component';


const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'about-us', component: AboutUsComponent },
  { path: 'contact-us', component: ContactUsComponent },
  { path: 'services', component: ServicesComponent },
  { path: 'test-error', component: TestErrorComponent },
  { path: 'server-error', component: ServerErrorComponent },
  { path: 'not-found', component: NotFoundComponent },
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(module => module.ShopModule)},
  { path: 'basket', loadChildren: () => import('./basket/basket.module').then(module => module.BasketModule)},
  { path: 'checkout', canActivate: [AuthGuard], loadChildren: () => import('./checkout/checkout.module')
  .then(module => module.CheckoutModule)},
  { path: 'orders', canActivate: [AuthGuard], loadChildren: () => import('./orders/orders.module')
  .then(module => module.OrdersModule)},
  { path: 'account', loadChildren: () => import('./account/account.module')
  .then(module => module.AccountModule)},
  { path: 'admin-console', loadChildren: () => import('./admin-console/admin-console.module')
  .then(module => module.AdminConsoleModule)},
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
