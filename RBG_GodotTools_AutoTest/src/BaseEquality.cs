namespace RBG_GodotTools_AutoTest;

using System;
using Chickensoft.GoDotTest;
using Godot;
using Shouldly;

/// <summary>
/// Runs all the basic checks for a class that inherits IEquatable.
/// </summary>
/// <typeparam name="T">Type for IEquatable class to run tests for.</typeparam>
/// <typeparam name="TOther">Type for other IEquatable class to compare to.</typeparam>
public abstract class BaseEquality<T, TOther> : TestClass where T : class, IEquatable<T> where TOther : IEquatable<TOther> {
  /// <summary>
  /// Constructor for RBG_AutoTest_BaseEquality class.
  /// </summary>
  /// <param name="testScene">Input for Chickensoft class</param>
  protected BaseEquality(Node testScene) : base(testScene) {
  }

  /// <summary>
  /// Creates a new instance of object of type T. Should be "default" or empty.
  /// </summary>
  /// <returns>New default instance of type T.</returns>
  public abstract T MakeDefaultValue();
  /// <summary>
  /// Creates a new instance of object of type T. Should have all data filled in.
  /// </summary>
  /// <returns>New instance with complete set of data of type T.</returns>
  public abstract T MakeFilledValue();
  /// <summary>
  /// Method that calls T? == T?. This is used because otherwise it will always run object? == object?.
  /// </summary>
  /// <param name="lho">left hand operand</param>
  /// <param name="rho">right hand operand</param>
  /// <returns>boolean result of T? == T?</returns>
  public abstract bool RunEqualsOperator(T? lho, T? rho);
  /// <summary>
  /// Method that calls T? != T?. This is used because otherwise it will always run object? != object?.
  /// </summary>
  /// <param name="lho">left hand operand</param>
  /// <param name="rho">right hand operand</param>
  /// <returns>boolean result of T? != T?</returns>
  public abstract bool RunNotEqualsOperator(T? lho, T? rho);

  /// <summary>
  /// Creates a new instance of a type different than T. To test Equals(object?) method.
  /// </summary>
  /// <returns>Object instance that has type not of type T.</returns>
  public abstract TOther MakeOtherType();

  /// <summary>
  /// When T? == T? and both values are null, it should return true.
  /// </summary>
  [Test]
  public void BothNull_EqualsOperator() {
    T? obj1 = null;
    T? obj2 = null;

    RunEqualsOperator(obj1, obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? != T? and both values are null, it should return false.
  /// </summary>
  [Test]
  public void BothNull_NotEqualsOperator() {
    T? obj1 = null;
    T? obj2 = null;

    RunNotEqualsOperator(obj1, obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1 == T? obj2 and only obj1 is null, it should return false.
  /// </summary>
  [Test]
  public void Obj1IsNull_EqualsOperator() {
    T? obj1 = null;
    var obj2 = MakeFilledValue();

    RunEqualsOperator(obj1, obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1 != T? obj2 and only obj1 is null, it should return true.
  /// </summary>
  [Test]
  public void Obj1IsNull_NotEqualsOperator() {
    T? obj1 = null;
    var obj2 = MakeFilledValue();

    RunNotEqualsOperator(obj1, obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1 == T? obj2 and only obj2 is null, it should return false.
  /// </summary>
  [Test]
  public void Obj2IsNull_EqualsOperator() {
    var obj1 = MakeFilledValue();
    T? obj2 = null;

    RunEqualsOperator(obj1, obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1.Equals(T? obj2) and only obj2 is null, it should return false.
  /// </summary>
  [Test]
  public void Obj2IsNull_EqualsMethod() {
    var obj1 = MakeFilledValue();
    T? obj2 = null;

    obj1.Equals(obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1 != T? obj2 and only obj2 is null, it should return true.
  /// </summary>
  [Test]
  public void Obj2IsNull_NotEqualsOperator() {
    var obj1 = MakeFilledValue();
    T? obj2 = null;

    RunNotEqualsOperator(obj1, obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1.Equals(object? obj2) and only obj2 is null, it should return false.
  /// </summary>
  [Test]
  public void Obj2IsNullAsObject_EqualsMethod() {
    var obj1 = MakeFilledValue();
    object? obj2 = null;

    obj1.Equals(obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1.Equals(object? obj2) and obj2 is a different type, it should return false.
  /// </summary>
  [Test]
  public void Obj2IsDifferentType_EqualsMethod() {
    var obj1 = MakeFilledValue();
    var obj2 = MakeOtherType();

    obj1.Equals(obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1 == T? obj2 and obj 1 and obj2 are different values, it should return false.
  /// </summary>
  [Test]
  public void DefaultComparedToFull_EqualsOperator() {
    var obj1 = MakeDefaultValue();
    var obj2 = MakeFilledValue();

    RunEqualsOperator(obj1, obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1.Equals(T? obj2) and obj 1 and obj2 are different values, it should return false.
  /// </summary>
  [Test]
  public void DefaultComparedToFull_EqualsMethod() {
    var obj1 = MakeDefaultValue();
    var obj2 = MakeFilledValue();

    obj1.Equals(obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1 != T? obj2 and obj 1 and obj2 are different values, it should return true.
  /// </summary>
  [Test]
  public void DefaultComparedToFull_NotEqualsOperator() {
    var obj1 = MakeDefaultValue();
    var obj2 = MakeFilledValue();

    RunNotEqualsOperator(obj1, obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When obj 1 and obj2 are different values, they should return different Hash Values.
  /// </summary>
  [Test]
  public void DefaultComparedToFull_HasDifferentHash() {
    var obj1 = MakeDefaultValue();
    var obj2 = MakeFilledValue();

    (obj1.GetHashCode() == obj2.GetHashCode()).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1.Equals(object? obj2) and obj 1 and obj2 are different values, it should return false.
  /// </summary>
  [Test]
  public void DefaultComparedToFullAsObject_EqualsMethod() {
    var obj1 = MakeDefaultValue();
    object? obj2 = MakeFilledValue();

    obj1.Equals(obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When T? obj1 == T? obj2 and obj 1 and obj2 reference the same object instance, it should return true.
  /// </summary>
  [Test]
  public void SameReference_EqualsOperator() {
    var obj1 = MakeFilledValue();
    var obj2 = obj1;

    RunEqualsOperator(obj1, obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1.Equals(T? obj2) and obj 1 and obj2 reference the same object instance, it should return true.
  /// </summary>
  [Test]
  public void SameReference_EqualsMethod() {
    var obj1 = MakeFilledValue();
    var obj2 = obj1;

    obj1.Equals(obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1 != T? obj2 and obj 1 and obj2 reference the same object instance, it should return false.
  /// </summary>
  [Test]
  public void SameReference_NotEqualsOperator() {
    var obj1 = MakeFilledValue();
    var obj2 = obj1;

    RunNotEqualsOperator(obj1, obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When obj 1 and obj2 reference the same object instance, they should return the same Hash Values.
  /// </summary>
  [Test]
  public void SameReference_HasSameHash() {
    var obj1 = MakeFilledValue();
    var obj2 = obj1;

    (obj1.GetHashCode() == obj2.GetHashCode()).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1.Equals(object? obj2) and obj 1 and obj2 reference the same object instance, it should return true.
  /// </summary>
  [Test]
  public void SameReferenceAsObject_EqualsMethod() {
    var obj1 = MakeFilledValue();
    object? obj2 = obj1;

    obj1.Equals(obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1 == T? obj2 and obj 1 and obj2 reference different object instances
  /// with the same data, it should return true.
  /// </summary>
  [Test]
  public void SameData_EqualsOperator() {
    var obj1 = MakeFilledValue();
    var obj2 = MakeFilledValue();

    RunEqualsOperator(obj1, obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1.Equals(T? obj2) and obj 1 and obj2 reference different object instances
  /// with the same data, it should return true.
  /// </summary>
  [Test]
  public void SameData_EqualsMethod() {
    var obj1 = MakeFilledValue();
    var obj2 = MakeFilledValue();

    obj1.Equals(obj2).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1 != T? obj2 and obj 1 and obj2 reference different object instances
  /// with the same data, it should return false.
  /// </summary>
  [Test]
  public void SameData_NotEqualsOperator() {
    var obj1 = MakeFilledValue();
    var obj2 = MakeFilledValue();

    RunNotEqualsOperator(obj1, obj2).ShouldBeFalse();
  }

  /// <summary>
  /// When obj 1 and obj2 reference different object instances with the same data,
  /// they should return the same Hash Values.
  /// </summary>
  [Test]
  public void SameData_HasSameHash() {
    var obj1 = MakeFilledValue();
    var obj2 = MakeFilledValue();

    (obj1.GetHashCode() == obj2.GetHashCode()).ShouldBeTrue();
  }

  /// <summary>
  /// When T? obj1.Equals(object? obj2) and obj 1 and obj2 reference different object instances
  /// with the same data, it should return true.
  /// </summary>
  [Test]
  public void SameDataAsObject_EqualsMethod() {
    var obj1 = MakeFilledValue();
    object? obj2 = MakeFilledValue();

    obj1.Equals(obj2).ShouldBeTrue();
  }
}
