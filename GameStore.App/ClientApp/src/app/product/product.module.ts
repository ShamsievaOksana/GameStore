import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductComponent } from './product.component';
import { ProductDetailsComponent } from './product-details/product-details.component';
import { RouterModule } from '@angular/router';
import { ProductService } from './product-service';

@NgModule({
  declarations: [ProductComponent, ProductDetailsComponent],
  imports: [
    CommonModule,
    RouterModule.forRoot([
      { path: 'products', component: ProductComponent }
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
