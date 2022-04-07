import { Component, Input, OnInit } from '@angular/core';
import { Testimonial } from './testimonial';

@Component({
  selector: 'sf-testimonial',
  templateUrl: './testimonial.component.html',
  styles: []
})
export class TestimonialComponent implements OnInit {
  @Input()
  public model: string | undefined;

  public testimonial: Testimonial | undefined;
  ngOnInit() {
      if (this.model)
          this.testimonial = JSON.parse(this.model);
  }
}
