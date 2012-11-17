define [ 
    'ExampleModel'
], (ExampleModel) ->
    describe 'exemple model', ->
        it 'should create a new object', ->
            model = new ExampleModel()
            expect(model).not.toBeNull()