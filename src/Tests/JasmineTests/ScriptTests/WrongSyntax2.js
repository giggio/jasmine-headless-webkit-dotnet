describe "Calculator", ->
    it "should sum 2 + 1 and return 3", ->
        expect(new Calculator().sum(1,2)).toBe 3