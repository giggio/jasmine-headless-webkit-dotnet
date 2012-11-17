define [ 
    'ExampleModelWithText'    
], (ExampleModel) ->
    describe 'example model with text on a attribute', ->
        model = null
        beforeEach ->
            model = new ExampleModel()
        it 'should create the model', ->            
            expect(model).not.toBeNull()
        it 'should find the template in the model', ->
            expect(model.myTemplate).toBe('ExampleTemplate')