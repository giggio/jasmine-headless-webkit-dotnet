(function() {

  define(['text!template/ExemploTemplate.html'], function(exampleTemplate) {
    return describe('text for requirejs', function() {
      return it('should find the template', function() {
        return expect(exampleTemplate).toBe('ExampleTemplate');
      });
    });
  });

}).call(this);
