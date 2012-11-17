(function() {

  define(['ExampleModel'], function(ExampleModel) {
    return describe('exemple model', function() {
      return it('should create a new object', function() {
        var model;
        model = new ExampleModel();
        return expect(model).not.toBeNull();
      });
    });
  });

}).call(this);
