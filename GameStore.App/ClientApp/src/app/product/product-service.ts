import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { ProductModel } from "./product-model";
import { ProductModule } from "./product.module";

@Injectable()
export class ProductService {
    constructor(private http: HttpClient) { 

    }

    getProducts():Observable<ProductModel[]>{
        return this.http.get<ProductModel[]>('api/product');
    }

    getProduct(productId:number):Observable<ProductModule>{
        return this.http.get<ProductModel>('api/product/' + productId );
    }
}
