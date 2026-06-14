import { Component, EventEmitter, Input, OnInit, Optional, Output, Self } from '@angular/core';
import { NgControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-custom-input',
  imports: [],
  templateUrl: './custom-input.html',
  styleUrl: './custom-input.css',
})
export class CustomInput{

  @Input() value = '';
  @Input() placeholder = '';
  @Input() label = '';
  @Input() type = "";
  @Input() required = false;

  @Output() valueChange = new EventEmitter<string>();

  onInput(event: Event) {
    this.valueChange.emit((event.target as HTMLInputElement).value);
  }
}
