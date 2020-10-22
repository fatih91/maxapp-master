import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

// Other imports
import { TestBed } from '@angular/core/testing';
import {HttpClient, HttpRequest, HttpResponse} from '@angular/common/http';
import {SymptomService} from "./symptom.service";
import {Symptom} from "../models/symptom.model";
import {DiagnoseService} from "./diagnose.service";
import {Diagnose} from "../models/diagnose.model";
import {BrowserDynamicTestingModule, platformBrowserDynamicTesting} from "@angular/platform-browser-dynamic/testing";
import {get} from "https";
import * as http from "http";

describe('DiagnoseService', () => {
  let httpClient: HttpClient;
  let httpTestingController: HttpTestingController;
  let diagnoseService: DiagnoseService;
  let httpClientSpy: { get: jasmine.Spy };

  beforeEach(() => {
    TestBed.configureTestingModule({
      // Import the HttpClient mocking services
      imports: [ HttpClientTestingModule ],
      // Provide the service-under-test and its dependencies
      providers: [
        DiagnoseService,
       // HttpErrorHandler,
       // MessageService
      ]
    });

    // Inject the http, test controller, and service-under-test
    // as they will be referenced by each test.
    httpClient = TestBed.get(HttpClient);
    httpTestingController = TestBed.get(HttpTestingController);
    diagnoseService = TestBed.get(DiagnoseService);
  });
  afterEach(() => {
    // After every test, assert that there are no more pending requests.
    httpTestingController.verify();
  });

  /// HeroService method tests begin ///

    //it('should return expected heroes (called once)', () => {

    // symptomService.getSymptom(1).subscribe(
    //   symptom => expect(symptom).toEqual(expectedSymptom, 'should return expected symptom'),
    //   fail
    // );

  describe('#getHeroes', () => {
    let expectedDiagnoses: Diagnose[];

    beforeEach(() => {
      diagnoseService = TestBed.get(DiagnoseService);
      expectedDiagnoses = [
    //    {diagnoseId: 1, term: "1", icd: "1", technicalTerm: "1"}] as Diagnose[];

    });

    it('should return expected heroes (called once)', () => {

      diagnoseService.getDiagnose(1).subscribe(
        diagnose => expect(diagnose.diagnoseId).toEqual(expectedDiagnoses[0].diagnoseId, 'should return expected heroes'),
        fail
      );

      // HeroService should have made one request to GET heroes from expected URL
     // const req = httpTestingController.expectOne(diagnoseService.diagnoseEndpoint);
  //    let requests = httpTestingController.match('https://localhost:5001/api/diagnoses', method: "");
      //expect(requests.length).toBe(2);

      //expect(requests.length).toBe(2);

      // Respond with the mock heroes
     // req.flush(expectedDiagnoses);
    });










    // HeroService should have made one request to GET heroes from expected URL
    // const req = httpTestingController.expectOne(symptomService.e);
    // expect(req.request.method).toEqual('GET');

    // Respond with the mock heroes
    // req.flush(expectedHeroes);

});});
