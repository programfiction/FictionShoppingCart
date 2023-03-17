import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent {
  @Input() pageNumber?: number;
  @Input() pageSize?: number;
  @Input() totalCount?: number;
  @Output() PageChanged = new EventEmitter<number>();

  onPageChanged(event: any) {
    this.PageChanged.emit(event.page);
  }
}
