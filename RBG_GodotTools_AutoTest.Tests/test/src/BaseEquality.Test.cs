namespace RBG_GodotTools_AutoTest.Tests;

using Godot;

public class BaseEqualityTest_String : BaseEquality<string, int> {
  public BaseEqualityTest_String(Node testScene) : base(testScene) {
  }

  public override string MakeDefaultValue() => "";
  public override string MakeFilledValue() => "String with data.";
  public override int MakeOtherType() => 42;
  public override bool RunEqualsOperator(string? lho, string? rho) => lho == rho;
  public override bool RunNotEqualsOperator(string? lho, string? rho) => lho != rho;
}
