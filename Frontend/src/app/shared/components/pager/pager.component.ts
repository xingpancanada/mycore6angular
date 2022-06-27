import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {
  @Input() count: number = 0;
  @Input() pageSize: number = 100;
  @Input() page: number = 1;
  @Output() pageChanged = new EventEmitter<number>();
  constructor() { }

  ngOnInit(): void {
  }

  onPageChange(event: any){
    this.pageChanged.emit(event.page);
  }
}
