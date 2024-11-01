namespace RBG_GodotTools_AutoTest;

using System;
using Chickensoft.GoDotTest;
using Godot;
using Shouldly;

/// <summary>
/// Run one set of edge case equality/inequality tests for an class that inherits IEquatable.
/// </summary>
/// <typeparam name="T">Type for IEquatable class to run tests for.</typeparam>
public abstract class EdgeCaseComparisonEquality<T> : TestClass where T : class, IEquatable<T> {
  /// <summary>
  /// Constructor for EdgeCaseComparisonEquality class.
  /// </summary>
  /// <param name="testScene">Input for Chickensoft class</param>
  protected EdgeCaseComparisonEquality(Node testScene) : base(testScene) {
  }

  /// <summary>
  /// Should create one newly instantiated object to compare to MakeObj2 Method's return value.
  /// </summary>
  public abstract T MakeObj1();

  /// <summary>
  /// Should create one newly instantiated object to compare to MakeObj1 Method's return value.
  /// </summary>
  public abstract T MakeObj2();

  /// <summary>
  /// Method that calls lho == rho with the signature T? == T?. This is used because otherwise it will always run object? == object?.
  /// </summary>
  /// <param name="lho">left hand operator</param>
  /// <param name="rho">right hand operator</param>
  /// <returns>boolean result of lho == rho</returns>
  public abstract bool RunEqualsOperator(T? lho, T? rho);

  /// <summary>
  /// Method that calls lho != rho with the signature T? != T?. This is used because otherwise it will always run object? != object?.
  /// </summary>
  /// <param name="lho">left hand operator</param>
  /// <param name="rho">right hand operator</param>
  /// <returns>boolean result of lho == rho</returns>
  public abstract bool RunNotEqualsOperator(T? lho, T? rho);

  /// <summary>
  /// Set to true to check values that should be equal, false for values that should not be equal.
  /// </summary>
  public bool IsEqual;

  /// <summary>
  /// Tests that == operator returns the expected value based on IsEqual.
  /// IsEqual is true then == operator is expected to return true,
  /// IsEqual is false then == operator is expected to return false.
  /// </summary>
  [Test]
  public void EqualsOperator() {
    var obj1 = MakeObj1();
    var obj2 = MakeObj2();

    RunEqualsOperator(obj1, obj2).ShouldBe(IsEqual);
  }

  /// <summary>
  /// Tests that .Equals(T? val) returns the expected value based on IsEqual.
  /// IsEqual is true then .Equals(T? val) is expected to return true,
  /// IsEqual is false then .Equals(T? val) is expected to return false.
  /// </summary>
  [Test]
  public void EqualsMethod() {
    var obj1 = MakeObj1();
    var obj2 = MakeObj2();

    obj1.Equals(obj2).ShouldBe(IsEqual);
  }

  /// <summary>
  /// Tests that != operator returns the expected value based on IsEqual.
  /// IsEqual is true then != operator is expected to return false,
  /// IsEqual is false then != operator is expected to return true.
  /// </summary>
  [Test]
  public void NotEqualsOperator() {
    var obj1 = MakeObj1();
    var obj2 = MakeObj2();

    RunNotEqualsOperator(obj1, obj2).ShouldBe(!IsEqual);
  }

  /// <summary>
  /// Tests that .Equals(object? val) returns the expected value based on IsEqual.
  /// IsEqual is true then .Equals(object? val) is expected to return true,
  /// IsEqual is false then .Equals(object? val) is expected to return false.
  /// </summary>
  [Test]
  public void AsObject_EqualsMethod() {
    var obj1 = MakeObj1();
    object? obj2 = MakeObj2();

    obj1.Equals(obj2).ShouldBe(IsEqual);
  }
}
