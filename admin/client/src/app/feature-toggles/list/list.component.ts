import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FeatureTogglesService } from '../feature-toggles.service';
import { ToggleListModel } from 'src/app/models/toggle-list-model';

@Component({
  selector: 'app-feature-toggles-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.sass']
})
export class FeatureTogglesListComponent implements OnInit {
  toggleModels: ToggleListModel[];

  constructor(private route: ActivatedRoute, private featureTogglesService: FeatureTogglesService) { }

  ngOnInit() {
    this.featureTogglesService.getAll().subscribe(data => this.toggleModels = data.map(toggle => new ToggleListModel(toggle)));
  }

  doToggle(model: ToggleListModel) {
    const toggle = model.toggle;
    model.changing = true;
    this.featureTogglesService.enable(toggle, !toggle.enabled)
      .subscribe(_ => {
        toggle.enabled = !toggle.enabled;
        model.changing = false;
      }, err => {
        // TODO: handle errors
        model.changing = false;
      });
  }

  deleteToggle(model: ToggleListModel) {
    const toggle = model.toggle;
    model.changing = true;
    this.featureTogglesService.delete(toggle).subscribe(_ => {
      const index = this.toggleModels.indexOf(model);
      if (index >= 0) {
        this.toggleModels.splice(index, 1);
      }
    }, err => {
      // TODO: handle errors
      model.changing = false;
    });
  }
}
