import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-form-button',
  imports: [],
  templateUrl: './form-button.html',
  styleUrl: './form-button.css',
})
export class FormButton {
    @Input() disabled = false;
}
