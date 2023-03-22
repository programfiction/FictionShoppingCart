import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { Product } from 'src/app/shared/models/product';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute, private bcS: BreadcrumbService, private bS: BasketService) {
    this.bcS.set('@productDetails',' ');
  }
  ngOnInit(): void {
    this.loadProduct();
  }
  loadProduct() {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (id) this.shopService.getProduct(+id).subscribe({
      next:  product =>{ 
        this.product = product;
        this.bcS.set('@productDetails',this.product?.name);
      },
      error: e => console.log(e)
    })
  }
  addItemToBasket() {
    this.product && this.bS.addItemToBasket(this.product);
  }


}
