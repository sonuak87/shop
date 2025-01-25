import { Component, inject } from '@angular/core';
import { ShopComponent } from '../../../features/shop/shop.component';
import { ShopService } from '../../../core/services/shop.service';
import { MatButton } from '@angular/material/button';
import { MatDivider } from '@angular/material/divider';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-filters-dialog',
  imports: [
    MatButton,
    MatDivider,
    MatSelectionList,
    MatListOption, FormsModule
    ],
  templateUrl: './filters-dialog.component.html',
  styleUrl: './filters-dialog.component.scss'
})
export class FiltersDialogComponent {
  shopService = inject(ShopService);
  private dialogRef = inject(MatDialogRef<FiltersDialogComponent>);
  data = inject(MAT_DIALOG_DATA); 

  selectedBrands: string[] = this.data.selectedBrands;
  selectedTypes: string[] = this.data.selectedTypes;

  applyFilter() {  
    this.dialogRef.close({
    selectedTypes: this.selectedTypes,
    selectedBrands: this.selectedBrands,
    })
}


}
