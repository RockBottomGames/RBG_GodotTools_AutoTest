namespace RBG_GodotTools_AutoTest.Tests;

using Chickensoft.GoDotTest;
using Godot;

public class EdgeCaseComparisonEqualityTest : TestClass {
  public EdgeCaseComparisonEqualityTest(Node testScene) : base(testScene) { }

  public class EdgeCaseComparisonEquality_StringNotEqual : EdgeCaseComparisonEquality<string> {
    public EdgeCaseComparisonEquality_StringNotEqual(Node testScene) : base(testScene) {
      IsEqual = false;
    }

    public override string MakeObj1() => "String One";
    public override string MakeObj2() => "String Two";
    public override bool RunEqualsOperator(string? lho, string? rho) => lho == rho;
    public override bool RunNotEqualsOperator(string? lho, string? rho) => lho != rho;
  }

  public class EdgeCaseComparisonEquality_StringEqual : EdgeCaseComparisonEquality<string> {
    public EdgeCaseComparisonEquality_StringEqual(Node testScene) : base(testScene) {
      IsEqual = true;
    }

    public override string MakeObj1() => "String Same";
    public override string MakeObj2() => "String Same";
    public override bool RunEqualsOperator(string? lho, string? rho) => lho == rho;
    public override bool RunNotEqualsOperator(string? lho, string? rho) => lho != rho;
  }
}
