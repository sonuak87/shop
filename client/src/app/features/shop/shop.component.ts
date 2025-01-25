import { SortParams } from '../../shared/models/sortParams';
import { Component, inject, OnInit } from '@angular/core';
import { ShopService } from '../../core/services/shop.service';
import { Product } from '../../shared/models/product';
import { ProductItemComponent } from "./product-item/product-item.component";
import { MatDialog } from '@angular/material/dialog';
import { FiltersDialogComponent } from '../../featurs/shop/filters-dialog/filters-dialog.component';
import { MatIcon } from '@angular/material/icon';
import { MatButton } from '@angular/material/button';
import { MatMenu, MatMenuTrigger } from '@angular/material/menu';
import { MatListOption, MatSelectionList, MatSelectionListChange } from '@angular/material/list';
import {MatPaginatorModule, PageEvent} from '@angular/material/paginator';
import { Pagination } from '../../shared/models/pagination';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-shop',
  imports: [MatButton, ProductItemComponent, MatIcon,
    MatMenu, MatSelectionList,
    MatListOption, MatMenuTrigger,
    MatPaginatorModule,
    FormsModule
  ],
  templateUrl: './shop.component.html',
  styleUrl: './shop.component.scss'
})
export class ShopComponent implements OnInit {
  products?: Pagination<Product>;
  private services = inject(ShopService);
  private dialogService = inject(MatDialog);
  shopParams = new SortParams();

  sortOptions = [
    { name: 'Alphabetical', value: 'name' },
    { name: 'Price:Low-Heigh', value: 'priceAsc' },
    { name: 'Price: Heigh-Low', value: 'priceDesc' }
  ];
  pageSizeOptions = [5, 10, 15, 20];

  onSortChange(event:MatSelectionListChange) {
    const selectedOption = event.options[0];
    if (selectedOption) {
      this.shopParams.sort = selectedOption.value;
      this.shopParams.pageNumber = 1;
      this.getProducts();
    }
  }
  
  ngOnInit(): void {
    this.initailizeShop();
  }

  initailizeShop() {
    this.services.getBrand();
    this.services.getType();
    this.getProducts();
  }
  
  onSearchChange() {
    this.shopParams.pageNumber = 1;
    this.getProducts();
  
}
  handlePageEvent(event: PageEvent) {
    this.shopParams.pageNumber=event.pageIndex+1
    this.shopParams.pageSiz = event.pageSize; 
    this.getProducts();
  }
  getProducts() {
     this.services.getProduct(this.shopParams).subscribe({
       next: response => this.products = response,
      error: error => console.log(error)
    });
  }
  openFilterDialog() {
    const dialogRef = this.dialogService.open(FiltersDialogComponent, {
      minWidth: '500px',
      data: {
        selectedBrands: this.shopParams.brands,
        selectedTypes: this.shopParams.types
      }
    });
    dialogRef.afterClosed().subscribe({
      next: result => {
        if (result) {         
        this.shopParams.brands = result.selectedBrands;
          this.shopParams.types = result.selectedTypes;
          this.shopParams.pageNumber = 1;
          this.getProducts();
        }
      }
    })
 }
}
