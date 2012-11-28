/// <reference path="Support/jasmine-1.2.d.ts" />
/// <reference path="../Scripts/calculator.ts" />

declare var Calculator: any;
declare var describe: any;
declare var expect: any;
declare var it: any;

describe("Calculator", function () {
    it("should fail", function () {
        expect(new Calculator().sum(1, 2)).toBe(3);
    });
});