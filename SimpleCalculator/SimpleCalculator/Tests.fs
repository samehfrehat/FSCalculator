namespace SimpleCalculator.Program.Tests

open SimpleCalculator.Program
open NUnit.Framework

[<TestFixture>]
type Tests() =
    let simpleCalculator = SimpleCalculator()

    [<Test>]
    member Test.EmptyString_ShouldReturn_Zero() =
        let sum = simpleCalculator.Add ""
        Assert.AreEqual(0, sum)

    [<TestCase("1")>]
    member Test.OneNumber_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(1, sum)

    [<TestCase("1,2")>]
    member Test.TwoNumbers_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(1+2, sum)

    [<TestCase("1,2,4,6,9,12")>]
    member Test.UnknownAmountOfNumbers_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(1+2+4+6+9+12, sum)
    
    [<TestCase("1\n2,3")>]
    member Test.InputWithNewLines_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(1+2+3, sum)

    [<TestCase("//;\n11;22")>]
    member Test.InputWithCustomDelimiter_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(11+22, sum)

    [<TestCase("//;\n5;-5;5;-11")>]
    member Test.InputWithNigativeValues_ShouldReturn_Exception input =
        let negativeException = Assert.Throws(typeof<System.Exception>, (fun () -> simpleCalculator.Add input |> ignore))
        let expectedMessage = "negatives not allowed [-5; -11]"
        Assert.AreEqual(expectedMessage, negativeException.Message)

    [<TestCase("//;\n11;22;2000;1000;1001")>]
    member Test.SkipNumbersMoreThan1000_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(11+22+1000, sum)

    [<TestCase("//[;;;;]\n11;;;;22;;;;2000;;;;1000;;;;1001")>]
       member Test.InputContainLongDelimiter_ShouldReturn_Sum input =
           let sum = simpleCalculator.Add input
           Assert.AreEqual(11+22+1000, sum)

    [<TestCase("//[;][@][(]\n5;1@2(1000")>]
    member Test.InputContainDifferentSingleDelimiter_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(5+1+2+1000, sum)

    [<TestCase("//[;;][@@][({(]\n5;;1@@2({(1000")>]
    member Test.InputContainDifferentLongDelimiter_ShouldReturn_Sum input =
        let sum = simpleCalculator.Add input
        Assert.AreEqual(5+1+2+1000, sum)
