import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { TestErrorsComponent } from './shared/components/test-errors/test-errors.component';
import { NotFoundComponent } from './shared/components/not-found/not-found.component';
import { ValidationErrorComponent } from './shared/components/validation-error/validation-error.component';
import { ServerErrorComponent } from './shared/components/server-error/server-error.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'shop', component: ShopComponent },
    { path: 'shop/:id', component: ProductDetailsComponent },
     { path: 'test-errors', component: TestErrorsComponent },
     { path: 'not-found', component: NotFoundComponent },
     { path: 'validation-error', component: ValidationErrorComponent },
     { path: 'server-errors', component: ServerErrorComponent },
    { path: '**', redirectTo: 'not-found', pathMatch: 'full' }
];
