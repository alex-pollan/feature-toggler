import { Component, OnInit } from '@angular/core';
import { ApplicationsService } from '../services/applications.service';
import { TogglesApplication } from '../models/toggles-application';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-applications',
  templateUrl: './applications.component.html',
  styleUrls: ['./applications.component.sass']
})
export class ApplicationsComponent implements OnInit {
  applications: TogglesApplication[];
  newAppForm = new FormGroup({
    newAppId: new FormControl('', [Validators.required])
  });

  constructor(private applicationsService: ApplicationsService) { }

  ngOnInit() {
    this.applicationsService.getAll().subscribe(applications => {
      this.applications = applications;
    });
  }

  addNewApp() {
    const idFormControl = this.newAppForm.get('newAppId');
    this.applicationsService.add(new TogglesApplication(idFormControl.value, false));
    idFormControl.setValue('');
  }

  remove(application) {
    this.applicationsService.remove(application);
  }
}
