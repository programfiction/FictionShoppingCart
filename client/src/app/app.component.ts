import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Fiction Kart';

  constructor(private bS: BasketService) {

  }

  ngOnInit(): void {
    const basketId= localStorage.getItem('basket_id');
    if(basketId) this.bS.getBasket(basketId);
  }
}
