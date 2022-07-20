import { Injectable } from "@angular/core";
import { Testimonial } from "./testimonial";

@Injectable()
export class TestimonialParserService {
    public parse(data: string): Testimonial | null {
        try {
            const result = JSON.parse(data) as Testimonial;
            return result;
        } catch {
            return null;
        }
    }
}
