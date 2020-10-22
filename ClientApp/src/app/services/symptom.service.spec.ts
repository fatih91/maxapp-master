import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

// Other imports
import { TestBed } from '@angular/core/testing';
import { HttpClient, HttpResponse } from '@angular/common/http';
import {SymptomService} from "./symptom.service";
import {Symptom} from "../models/symptom.model";

describe('SymptomService', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let symptomService: SymptomService;
  let httpClientSpy: { get: jasmine.Spy };

  beforeEach(() => {
    TestBed.configureTestingModule({
      // Import the HttpClient mocking services
      imports: [HttpClientTestingModule],
      // Provide the service-under-test and its dependencies
      providers: [
        SymptomService,
        // HttpErrorHandler,
        // MessageService
      ]
    });

    // Inject the http, test controller, and service-under-test
    // as they will be referenced by each test.
    httpClient = TestBed.get(HttpClient);
    httpTestingController = TestBed.get(HttpTestingController);
    symptomService = TestBed.get(SymptomService);
  });
  afterEach(() => {
    // After every test, assert that there are no more pending requests.
    httpTestingController.verify();
  });

  /// HeroService method tests begin ///

  describe('#getSymptom', () => {
    let expectedSymptom: Symptom;

    beforeEach(() => {
      symptomService = TestBed.get(SymptomService);
      expectedSymptom =
        { symptomId: 1, technicalTerm: 'TT', term: "t" };
    });


    //it('should return expected heroes (called once)', () => {

     // symptomService.getSymptom(1).subscribe(
     //   symptom => expect(symptom).toEqual(expectedSymptom, 'should return expected symptom'),
     //   fail
     // );

    it('should return expected symptom (HttpClient called once)', () => {
      const expectedSymptom: Symptom =
        { symptomId: 1, term: "1", definition: "1", technicalTerm: "1" };

      //httpClientSpy.get.and.returnValue(asyncData(expectedHeroes));

      symptomService.getSymptom(1).subscribe(
        symptom => expect(symptom.symptomId).toEqual(expectedSymptom.symptomId, 'expected smyptoms'),
        fail
      );
      expect(httpClientSpy.get.calls.count()).toBe(1, 'one call');
    });

      // HeroService should have made one request to GET heroes from expected URL
     // const req = httpTestingController.expectOne(symptomService.e);
     // expect(req.request.method).toEqual('GET');

      // Respond with the mock heroes
     // req.flush(expectedHeroes);
    });

});
