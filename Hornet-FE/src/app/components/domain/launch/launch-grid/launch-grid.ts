import { Component, Input, Output, EventEmitter, computed } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LaunchComponent } from '../launch/launch';
import { Launch } from '@services/core/launch/Launch';

@Component({
  selector: 'app-launch-grid',
  standalone: true,
  imports: [CommonModule, LaunchComponent],
  templateUrl: './launch-grid.html',
})
export class LaunchGridComponent {
  @Input({ required: true }) title = '';
  @Input({ required: true }) launches: Launch[] = [];

  @Input({ required: true }) page = 1;
  @Input({ required: true }) size = 1;
  @Input({ required: false }) count: number  = 1;

  @Output() pageChange = new EventEmitter<number>();

  get hasNext() {
    return this.page * this.size < this.count;
  }

  totalPages = computed(() =>
   Math.ceil(this.count / this.size)
  );

  prev() {
    if (this.page > 1) {
      this.pageChange.emit(this.page - 1);
    }
  }

  next() {
    if (this.hasNext) {
      this.pageChange.emit(this.page + 1);
    }
  }

  goTo(page: number) {
    if (page !== this.page) {
      this.pageChange.emit(page);
    }
  }
}