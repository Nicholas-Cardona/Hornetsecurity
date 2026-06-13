import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-text-input',
  imports: [],
  templateUrl: './text-input.html',
  styleUrl: './text-input.css',
})
export class InputComponent {
  @Input() value = '';
  @Input() placeholder = '';
  @Input() label = '';
  @Input() type = "";

  @Output() valueChange = new EventEmitter<string>();

  onInput(event: Event) {
    this.valueChange.emit((event.target as HTMLInputElement).value);
  }
}
