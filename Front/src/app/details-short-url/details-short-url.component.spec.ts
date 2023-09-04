import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DetailsShortUrlComponent } from './details-short-url.component';

describe('DetailsShortUrlComponent', () => {
  let component: DetailsShortUrlComponent;
  let fixture: ComponentFixture<DetailsShortUrlComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [DetailsShortUrlComponent]
    });
    fixture = TestBed.createComponent(DetailsShortUrlComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
