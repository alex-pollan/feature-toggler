import { Component, OnInit, EventEmitter, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MessageService } from 'src/app/services/message.service';

@Component({
  selector: 'app-feature-toggles',
  templateUrl: './feature-toggles.component.html',
  styleUrls: ['./feature-toggles.component.sass']
})
export class FeatureTogglesComponent implements OnInit, OnDestroy {
  subscription: Subscription;
  justCreatedFeatureToggle: string;

  constructor(private messageService: MessageService) { }

  ngOnInit() {
    this.subscription = this.messageService.listen('feature-toggle').subscribe(message => {
      if (message.args['created']) {
        this.justCreatedFeatureToggle = message.args['created'];
        setTimeout(() => this.justCreatedFeatureToggle = null, 5000);
      }
    });
  }

  ngOnDestroy() {
    // unsubscribe to ensure no memory leaks
    this.subscription.unsubscribe();
  }
}
