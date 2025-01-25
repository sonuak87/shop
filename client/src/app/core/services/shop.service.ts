import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';
import { Pagination } from '../../shared/models/pagination';
import { SortParams } from '../../shared/models/sortParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  private http = inject(HttpClient);
  type: string[] = [];
  brand: string[] = [];

  getProductById(id: number) {
   return  this.http.get<Product>(this.baseUrl + 'products/'+id);
  }

  getProduct(shopParams:SortParams) {
    let params = new HttpParams();
    console.log(shopParams);
    if (shopParams.brands.length > 0) {
      params = params.append('brands', shopParams.brands.join(','));
    }
    
    if (shopParams.types.length > 0) {
      params = params.append('types', shopParams.types.join(','));
    }
    if (shopParams.sort) {
      params = params.append('sort', shopParams.sort)
    }
    if (shopParams.search) {
      params = params.append("search",shopParams.search);
    }

    params = params.append('pageSize', shopParams.pageSiz);
     params = params.append('pageIndex',shopParams.pageNumber);
    return  this.http.get<Pagination<Product>>(this.baseUrl + 'products', {params});
  }
  getType() {
    return this.http.get<string[]>(this.baseUrl + 'products/type')
      .subscribe({next:response=>this.type=response});
  }
  getBrand() {
    return this.http.get<string[]>(this.baseUrl + 'products/brand')
     .subscribe({next:response=>this.brand=response});
  } 
}
