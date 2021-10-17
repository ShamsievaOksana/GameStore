import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { ProductService } from './product-service';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatChipsModule } from '@angular/material/chips';

@NgModule({
  declarations: [ProductComponent, ProductDetailsComponent],
  imports: [
    CommonModule,
    MatCardModule,
    MatButtonModule,
    MatBadgeModule,
    MatChipsModule,
    RouterModule.forRoot([
      { path: 'products', component: ProductComponent },
      { path: 'products/:productId', component: ProductDetailsComponent }
    ])
  ],
  exports: [
    ProductComponent,
    ProductDetailsComponent
  ],
  providers: [
    ProductService
  ]
})
export class ProductModule { }
