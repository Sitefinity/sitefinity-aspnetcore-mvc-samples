import { Component, Input, OnInit } from '@angular/core';
import { Testimonial } from './testimonial';
import { TestimonialParserService } from './testimonial-parser.service';

@Component({
    selector: 'sf-testimonial',
    templateUrl: './testimonial.component.html',
    styles: []
})
export class TestimonialComponent implements OnInit {
    @Input()
    public model: string | undefined;

    public testimonial: Testimonial | null = null;

    constructor(private service: TestimonialParserService) {

    }

    ngOnInit() {
        if (this.model) {
            this.testimonial = this.service.parse(this.model);
        }
    }
}
