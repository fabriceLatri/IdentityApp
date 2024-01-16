import { TestBed } from '@angular/core/testing';

import { SharedService } from './shared.service';
import { ModalModule } from 'ngx-bootstrap/modal';

describe('SharedService', () => {
  let service: SharedService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [ModalModule.forRoot()],
    });
    service = TestBed.inject(SharedService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
