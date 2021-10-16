import { Component, OnInit } from '@angular/core';
import { ProductModel } from './product-model';
import { ProductService } from './product-service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  products: ProductModel[] 

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.productService.getProducts()
      .subscribe((data:ProductModel[])=>{
          this.products = data;
      })
  }

}
