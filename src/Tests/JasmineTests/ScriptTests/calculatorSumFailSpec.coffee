describe "Calculator", ->
    it "should fail", ->
        expect(new Calculator().sum(1,2)).toBe 4