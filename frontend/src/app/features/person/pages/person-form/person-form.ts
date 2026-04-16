import { Component, inject, signal } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { PersonService } from '../../services/person';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-person-form',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './person-form.html',
  styleUrl: './person-form.css',
})
export class PersonForm {
  private fb = inject(FormBuilder);
  private service = inject(PersonService);
  private route = inject(ActivatedRoute);
  private router = inject(Router)

  id: number = Number(this.route.snapshot.paramMap.get('id'))
  isEdit = signal(!!this.id)

  form = this.fb.nonNullable.group({
    name: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(100)]],
    dateOfBirth: ['', Validators.required],
    heightInFeet: [0, [Validators.required, Validators.min(1), Validators.max(10)]],
    weightInKg: [0, [Validators.required, Validators.min(1), Validators.max(500)]],
    gender: ['', Validators.required],
    maritalStatus: ['', Validators.required],
    isGraduated: [false, Validators.required]
  })

  constructor() {
    if (this.isEdit()) {
      this.service.getById(this.id).subscribe(res => {
        this.form.patchValue(res);
      });
    }
  }

  submit() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    const value = this.form.getRawValue();
    const request = this.isEdit() ? this.service.update(this.id, value) : this.service.create(value);
    request.subscribe(() => this.router.navigate(['/']));
  }
}
