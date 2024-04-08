import { Component, OnInit, ViewChild, ElementRef  } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Component({
  selector: 'app-rectangle-component',
  templateUrl: './rectangle-component.component.html',
  styleUrls: ['./rectangle-component.component.css']
})
export class RectangleComponentComponent implements OnInit {
  svgDimensions: { width: number, height: number };
  drawing: boolean = false;
  startX: number;
  startY: number;
  width: number;
  height: number;
  perimeter: number = 0;
  // Set the fixed width and height for the drawing area
  drawingAreaWidth: number = 600;
  drawingAreaHeight: number = 400;

  constructor(private http: HttpClient) { }
  @ViewChild('drawingArea', { static: true }) drawingArea: ElementRef;

  ngOnInit(): void {
    this.http.get<{ width: number, height: number }>('https://localhost:44368/svgdimensions').subscribe((data: any) => {
      this.svgDimensions = data;
      this.width = this.svgDimensions.width;
      this.height = this.svgDimensions.height;
      // Draw rectangle immediately after fetching dimensions
      this.startDrawingInitialRectangle();
    });
    console.log(this.svgDimensions);
  }
  startDrawingInitialRectangle() {
    this.drawing = true;
    // Set initial mouse coordinates and rectangle dimensions
    this.startX = 0;
    this.startY = 0;
    // Update perimeter if needed
    this.perimeter = 2 * (this.width + this.height);
  }
  startDrawing(event: MouseEvent) {
    this.drawing = true;
    const boundingRect = this.drawingArea.nativeElement.getBoundingClientRect();
    this.startX = event.clientX - boundingRect.left;
    this.startY = event.clientY - boundingRect.top;
    this.width = 0;
    this.height = 0;
  }

  drawRectangle(event: MouseEvent) {
    if (!this.drawing) return;
    const boundingRect = this.drawingArea.nativeElement.getBoundingClientRect();
    this.width = event.clientX - boundingRect.left - this.startX;
    this.height = event.clientY - boundingRect.top - this.startY;
    this.calculatePerimeter();
  }


 

  updateJsonFile() {
    // Construct the payload with updated dimensions
    const payload = { width: this.width, height: this.height };
    // Convert the payload to a string
    const payloadAsString = JSON.stringify(payload);
    // Send a PUT request to the API endpoint to update the JSON file
    return this.http.put('https://localhost:44368/svgdimensions', payloadAsString, {
      headers: { 'Content-Type': 'application/json' } // Specify the content type as JSON

    });

  }


  stopDrawing() {
    if (!this.drawing) return;
    this.drawing = false;
    this.updateJsonFile().subscribe(() => {
      console.log('JSON file updated successfully!');
      // Redraw rectangle immediately after updating the JSON file
    });
    setTimeout(() => {
      this.http.get<{ width: number, height: number }>('https://localhost:44368/svgdimensions').subscribe((data: any) => {
        this.svgDimensions = data;
        this.width = this.svgDimensions.width;
        this.height = this.svgDimensions.height;
        // Draw rectangle immediately after fetching dimensions
        this.startDrawingInitialRectangle();
      });
    }, 1000); // Delay of 1 second
    
  }

  private calculatePerimeter() {
    this.perimeter = 2 * (this.width + this.height);
  }
}
