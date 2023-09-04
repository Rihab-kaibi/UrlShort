import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BulkUrlShortenerComponent } from './bulk-url-shortener.component';

describe('BulkUrlShortenerComponent', () => {
  let component: BulkUrlShortenerComponent;
  let fixture: ComponentFixture<BulkUrlShortenerComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [BulkUrlShortenerComponent]
    });
    fixture = TestBed.createComponent(BulkUrlShortenerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
