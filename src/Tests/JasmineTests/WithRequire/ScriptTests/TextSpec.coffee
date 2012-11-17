define [ 
    'text!template/ExemploTemplate.html'
], (exampleTemplate) ->
    describe 'text for requirejs', ->
        it 'should find the template', ->
            expect(exampleTemplate).toBe('ExampleTemplate')