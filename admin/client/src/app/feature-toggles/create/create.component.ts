import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, ValidatorFn, AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FeatureTogglesService } from '../feature-toggles.service';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { Toggle } from 'src/app/models/toggle';

@Component({
  selector: 'app-feature-toggles-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.sass']
})
export class FeatureTogglesCreateComponent implements OnInit {
  createForm: FormGroup;

  constructor(private featureTogglesService: FeatureTogglesService) { }

  ngOnInit() {
    this.createForm = new FormGroup({
      name: new FormControl('', Validators.required, this.duplicatedNameValidator()),
      description: new FormControl(''),
      enabled: new FormControl(false),
    });
  }

  onSubmit() {
    const toggle = {
      name: this.createForm.controls['name'].value,
      description: this.createForm.controls['description'].value,
      enabled: true
    };

    this.featureTogglesService.create(<Toggle>toggle)
      .subscribe(_ => {}, err => console.log(err));
  }

  duplicatedNameValidator(): AsyncValidatorFn {
    return (control: AbstractControl): Promise<ValidationErrors> => {
      return new Promise((resolve, reject) => {
        this.featureTogglesService.isDuplicated(control.value)
          .then(isValid => {
            if (isValid) {
              resolve(null);
              return;
            }

            resolve({ 'duplicated': { value: control.value } });
          }, err => reject(err));
      });
    };
  }
}
