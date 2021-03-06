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
  { path: '', component: HomeComponent, data: {breadcrumb: 'Home'} },
  { path: 'about-us', component: AboutUsComponent, data: {breadcrumb: 'About Us'} },
  { path: 'contact-us', component: ContactUsComponent, data: {breadcrumb: 'Contact Us'} },
  { path: 'services', component: ServicesComponent, data: {breadcrumb: 'Services'} },
  { path: 'test-error', component: TestErrorComponent, data: {breadcrumb: 'Test Errors'} },
  { path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: 'Server Errors'} },
  { path: 'not-found', component: NotFoundComponent, data: {breadcrumb: 'Not Found'} },
  { path: 'shop', loadChildren: () => import('./shop/shop.module').then(module => module.ShopModule), data: {breadcrumb: 'Shop'}},
  { path: 'basket', loadChildren: () => import('./basket/basket.module').then(module => module.BasketModule), data: {breadcrumb: 'Basket'}},
  { path: 'checkout', canActivate: [AuthGuard], loadChildren: () => import('./checkout/checkout.module')
  .then(module => module.CheckoutModule), data: {breadcrumb: 'Checkout'}},
  { path: 'orders', canActivate: [AuthGuard], loadChildren: () => import('./orders/orders.module')
  .then(module => module.OrdersModule), data: {breadcrumb: 'Orders'}},
  { path: 'account', loadChildren: () => import('./account/account.module')
  .then(module => module.AccountModule), data: {breadcrumb: {skip: true}}},
  { path: 'admin-console', loadChildren: () => import('./admin-console/admin-console.module')
  .then(module => module.AdminConsoleModule)},
  { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
