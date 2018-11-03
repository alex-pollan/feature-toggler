import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FeatureTogglesService } from '../feature-toggles.service';
import { Toggle } from 'src/app/models/toggle';

@Component({
  selector: 'app-feature-toggles',
  templateUrl: './feature-toggles.component.html',
  styleUrls: ['./feature-toggles.component.sass']
})
export class FeatureTogglesComponent implements OnInit {
  appId: string;
  toggles: Toggle[];

  constructor(private route: ActivatedRoute, private featureTogglesService: FeatureTogglesService) { }

  ngOnInit() {
    this.appId = this.route.snapshot.paramMap.get('appId');
    this.featureTogglesService.getAll(this.appId).subscribe(data => this.toggles = data.toggles);
  }

  doToggle(toggle: Toggle) {
    toggle.value = !toggle.value;
    // TODO: save
  }
}
