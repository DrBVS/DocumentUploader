import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-size',
  templateUrl: './size.component.html',
  styleUrls: ['./size.component.scss']
})
export class SizeComponent {

  private _size: number;
  formatedSize: number;
  unit: string;
  @Input() set size(size: number) {
    this._size = size;
    this.formatedSize = this._size / 1024;
    if (this.formatedSize < 100) {
      this.unit = 'KB';
    } else {
      this.formatedSize = this.formatedSize / 1024;
      this.unit = 'MB';
    }
  }

  constructor() { }
}
