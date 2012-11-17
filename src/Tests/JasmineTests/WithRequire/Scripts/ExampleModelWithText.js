(function() {

  define(['text!template/ExampleTemplate.html'], function(exampleTemplate) {
    var ExampleModel;
    return ExampleModel = (function() {

      function ExampleModel() {}

      ExampleModel.prototype.myTemplate = exampleTemplate;

      return ExampleModel;

    })();
  });

}).call(this);
