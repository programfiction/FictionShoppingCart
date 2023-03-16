import { Component, OnInit } from '@angular/core';
import { Brand } from '../shared/models/brand';
import { Product } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { Type } from '../shared/models/type';
import { ShopService } from './shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  brands: Brand[] = [];
  types: Type[] = [];
  shopParams: ShopParams = new ShopParams();
  sortOptions = [
    { name: 'Alphabetical: A to Z', value: 'nameAsc' },
    { name: 'Alphabetical: Z to A', value: 'nameDesc' },
    { name: 'Price: Low to High', value: 'priceAsc' },
    { name: 'Price: High to Low', value: 'priceDesc' },
  ];
  totalCount: number = 0;
  constructor(private shopService: ShopService) {

  }

  ngOnInit(): void {
    this.getProducts();
    this.getBrands();
    this.getTypes();
  }
  getProducts() {
    this.shopService.getProducts(this.shopParams).subscribe({
      next: r => {
        this.products = r.data;
        this.shopParams.pageNumber = r.pageIndex;
        this.shopParams.pageSize = r.pageSize;
        this.totalCount = r.count;
      },
      error: e => console.log(e)
    })
  }
  getBrands() {
    this.shopService.getBrands().subscribe({
      next: r => this.brands = [{ id: 0, name: 'All' }, ...r],
      error: e => console.log(e)
    })
  }
  getTypes() {
    this.shopService.getTypes().subscribe({
      next: r => this.types = [{ id: 0, name: 'All' }, ...r],
      error: e => console.log(e)
    })
  }

  onBrandSelected(brandId: number) {
    this.shopParams.brandId = brandId;
    this.getProducts();
  }
  onTypeSelected(typeId: number) {
    this.shopParams.typeId = typeId;
    this.getProducts();
  }
  onSortSelected(event: any) {
    this.shopParams.sort = event.target.value;
    this.getProducts();
  }

  onPageChanged(event: any) {
    if (this.shopParams.pageNumber !== event.page) {
      this.shopParams.pageNumber = event.page;
      this.getProducts();
    }
  }
}
