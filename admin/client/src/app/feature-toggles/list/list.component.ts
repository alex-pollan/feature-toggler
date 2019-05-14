import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FeatureTogglesService } from '../feature-toggles.service';
import { Toggle } from 'src/app/models/toggle';

@Component({
  selector: 'app-feature-toggles-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.sass']
})
export class FeatureTogglesListComponent implements OnInit {
  toggles: Toggle[];

  constructor(private route: ActivatedRoute, private featureTogglesService: FeatureTogglesService) { }

  ngOnInit() {
    this.featureTogglesService.getAll().subscribe(data => this.toggles = data);
  }

  doToggle(toggle: Toggle) {
    toggle.enabled = !toggle.enabled;
    // TODO: save
  }
}
