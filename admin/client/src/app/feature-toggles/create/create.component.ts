import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, ValidatorFn, AbstractControl, AsyncValidatorFn, ValidationErrors } from '@angular/forms';
import { Validators } from '@angular/forms';
import { FeatureTogglesService } from '../feature-toggles.service';
import { Toggle } from 'src/app/models/toggle';
import { ActivatedRoute, Router } from '@angular/router';
import { EventEmitter } from 'protractor';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-feature-toggles-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.sass']
})
export class FeatureTogglesCreateComponent implements OnInit {
  createForm: FormGroup;

  constructor(private featureTogglesService: FeatureTogglesService,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService) { }

  ngOnInit() {
    this.createForm = new FormGroup({
      name: new FormControl('', Validators.required, this.duplicatedNameValidator()),
      description: new FormControl(''),
      enabled: new FormControl(false),
    });
  }

  get name() { return this.createForm.get('name'); }

  get description() { return this.createForm.get('description'); }

  onSubmit() {
    const toggle = {
      name: this.name.value,
      description: this.description.value,
      enabled: true
    };

    this.featureTogglesService.create(<Toggle>toggle)
      .subscribe(_ => {
        this.messageService.broadcast('feature-toggle', { created: this.name.value });
        this.router.navigate(['../'], { relativeTo: this.route });
      }, err => console.log(err));
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
