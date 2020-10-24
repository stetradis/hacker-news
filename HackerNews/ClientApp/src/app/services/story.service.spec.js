"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var story_service_1 = require("./story.service");
describe('StoryService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [story_service_1.StoryService]
        });
    });
    it('should be created', testing_1.inject([story_service_1.StoryService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=story.service.spec.js.map