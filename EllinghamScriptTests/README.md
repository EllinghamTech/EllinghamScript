# Ellingham Script Tests
These tests take a small sample of EllinghamScript code, executes it then tests the
expected final state of the ScriptRunner.  We use NUnit here.

## Usage
To test valid code, create a method similar to the following:

```c#
[Test]
public void SimpleAssignAndOr()
{
    // Remember, except for brackets there is no order of operations at this time
    Script script = new Script("a = false || true; b = true && false;");
    ScriptRunner scriptRunner = new ScriptRunner(script);
    VarBase result = scriptRunner.Execute();
    TestHelpers.VariableCheck(scriptRunner, "a", true, typeof(VarBoolean));
    TestHelpers.VariableCheck(scriptRunner, "b", false, typeof(VarBoolean));
}
```

The script `a = false || true; b = true && false;` has a determined output, in all cases
`a` must be true and `b` must be false.

The `VariableCheck(ScriptRunner scriptRunner, string variableName, object value, Type variableType)` tests
to see if a variable in ScriptRunner has the expected value.

> Gotcha: A Number (VarNumber) is represented as a double, so always tell C# you are dealing
> with a double and not an int.  E.g. 10d, 1.5d, 1211.29128d

`VarBase result = scriptRunner.Execute();`

The result variable here is unused, however it has been particularly useful (as it makes life easier) when
debugging tests so although it is often seen as bad practice, there is method to the madness for this one.

## Organisation
Try to keep tests located in a relevant class.  Tests for invalid code or code where there is an exception
expected should be kept in a separate class to fully-working code.  (Testing failures is different to testing
for success!!!!!).

Method names should describe what the code does (or is) though not a paragraph for more complex tests.  For
example the method `SimpleAssignAndOr` tells you that it is not complex, it is testing assigns and it uses the
AND and OR operators.  We don't subscribe to the idea of also including expected results as part of the method
name as this should be obvious from the Asserts and/or TestHelpers used.

```
Good: SimpleAssignAndOr
Bad: AssigningTwoBooleansUsingAndOrOperatorsResultingInTrueAndFalseValues
```
I think it's quite obvious.