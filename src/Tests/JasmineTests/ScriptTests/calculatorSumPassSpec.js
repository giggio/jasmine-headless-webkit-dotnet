(function() {

  describe("Calculator", function() {
    return it("should sum 2 + 1 and return 3", function() {
      return expect(new Calculator().sum(1, 2)).toBe(3);
    });
  });

}).call(this);
