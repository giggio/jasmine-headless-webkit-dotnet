(function() {

  describe("Calculator", function() {
    return it("should fail", function() {
      return expect(new Calculator().sum(1, 2)).toBe(4);
    });
  });

}).call(this);
