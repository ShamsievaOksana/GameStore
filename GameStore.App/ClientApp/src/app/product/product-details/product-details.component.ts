import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductModel } from '../product-model';
import { ProductService } from '../product-service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  product: ProductModel | undefined;

  constructor(private route: ActivatedRoute, private productService: ProductService) { }

  ngOnInit() {
    const routeParams = this.route.snapshot.paramMap;
    const productId = Number(routeParams.get('productId'));

    this.productService.getProduct(productId)
      .subscribe((data:ProductModel)=>{
          this.product = data;
      });
  }

}
