(function() {

  define(['ExampleModelWithText'], function(ExampleModel) {
    return describe('example model with text on a attribute', function() {
      var model;
      model = null;
      beforeEach(function() {
        return model = new ExampleModel();
      });
      it('should create the model', function() {
        return expect(model).not.toBeNull();
      });
      return it('should find the template in the model', function() {
        return expect(model.myTemplate).toBe('ExampleTemplate');
      });
    });
  });

}).call(this);
