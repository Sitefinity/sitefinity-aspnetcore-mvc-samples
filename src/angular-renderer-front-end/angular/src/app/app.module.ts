import { Injector, NgModule } from '@angular/core';
import { createCustomElement, NgElementConfig } from '@angular/elements';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { TestimonialComponent } from './testimonial.component';

@NgModule({
  imports: [BrowserModule, BrowserAnimationsModule],
  declarations: [TestimonialComponent],
})
export class AppModule {
  constructor(private injector: Injector) {

  }

  ngDoBootstrap(){
    // Convert `PopupComponent` to a custom element.
    const elementConfig: NgElementConfig = { injector: this.injector };
    const PopupElement = createCustomElement(TestimonialComponent, elementConfig);
    // Register the custom element with the browser.
    customElements.define('sf-testimonial', PopupElement);
  }
}
