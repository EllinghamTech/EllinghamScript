**Work in progress - not everything has been finished yet**

# EllinghamScript
A much better implementation of the original PlatformScript.

## Getting Started
EllinghamScript is a C# .NET Core built scripting language.  It is not designed
for maximum performance but instead the ability to have a controlled and constrained
scripting language.

To use EllinghamScript in your .NET Core (or compatible) application, the recommended
approach is to use the nuget.

## License
Released under MIT license by Ellingham Innovations Ltd.  No warranty is provided.

We appreciate any support and attribution you wish to provide.  We welcome Pull Requests
and Issues on GitHub. 

## Writing EllinghamScript
EllinghamScript is pretty similar to other languages out there.  This readme file assumes
that you have a general understanding of programming/scripting languages.

### Variables
Variables have a type (they can also be "undefined") but you do not specify a type
when first declaring a variable.

```EllinghamScript
a = 5;
b = 17 + a;
c = "string";
d = myFunction();
```

#### Types
EllinghamScript has the following types built-in:
- Undefined (see VarBase)
- Boolean (see VarBoolean)
- Numeric (see VarNumeric)
- String (see VarString)
- Object (see VarObject)
    - NB: VarObject itself is an anonymous object however this can be modified
    by extending VarObject in C# and registering the new type with the preferred
    configuration.

The following table may be a useful reference:

| Type | Meaning | As a String | Boolean Value | Notes |
| --- | --- | --- | --- | --- |
| Undefined | Has no value | Empty String | false | |
| Boolean | True or false | True or False | true or false | |
| Numeric | A number, including whole and decimal numbers | The number as a string | false if 0 otherwise true | Uses the C# double type |
| String | Zero or more unicode characters | - | true unless empty | |
| Object | Contains properties and methods wrapped in a variable | JSON representation | true unless empty | |

### Functions
At this time, there is no way in EllinghamScript itself to create custom functions, however you can register
new functions in C# itself.  See "adding custom functions".  Note we define a method as a function
that is called within the context of an object.  To add a new method you need to create a custom variable type
by extending an existing type. See "adding new types".

Functions can have arguments and always have a return value (the undefined variable is the equivalent
to returning nothing/void, essentially).

### Methods
Most of the base types have pre-defined methods.  Objects can also contain additional methods and properties.

Methods and properties are always accessed on a variable using the accessor (.):

```EllinghamScript
a = -5;
b = a.abs(); // Absolute value of a, which is 5

if(a.isPositive()) { }

c = new object();
c.myProperty = "test";
```

All variables contain the following methods:

| Method Name | Meaning | Returns |
| --- | --- | --- |
| ToString() | The string value of the variable | String |
| ToBoolean() | The boolean value of the variable | Boolean |

#### Numeric Type
| Method Name | Meaning | Returns |
| --- | --- | --- |
| Abs() | The absolute value (positive value) | Positive numeric |
| IsPositive() | Tests to see if the value is positive | Boolean |
| IsNegative() | Tests to see if the value is negative | Boolean |


#### String Type
| Method Name | Meaning | Returns |
| --- | --- | --- |
| Trim() | Removes any whitespaces at the beginning and end of the string | String |
| ToUpper() | Removes any whitespaces at the beginning and end of the string | String |
| ToLower() | Removes any whitespaces at the beginning and end of the string | String |
| IsEmpty() | True if the string is empty | Boolean |
| Contains(s1) | True if the string contains s1 | Boolean |
| Length() | The length of the string | Numeric |
| Replace(s1, s2) | Replaces any occurrences of s1 with s2 | String |

#### Object Type
Objects can have additional methods defined within C# itself, which is demonstrated in the objects topic.  However
these are the built-in ones:

| Method Name | Meaning | Returns |
| --- | --- | --- |
| ToJson() | Returns a JSON string representation of the objects properties | String |

Anonymous objects can have custom properties defined within EllinghamScript, e.g:

```
a = new object();
a.title = "My Title";
a.age = 100;
```

*At this time, you cannot defined a method on an object from within EllinghamScript*

### Debugging & Logging
EllinghamScript has multiple logging modes:
- `Logging.None` - no logging
- `Logging.Basic` - logs only debug messages
- `Logging.Verbose` - logs debug messages with state information

Unlike other languages that use a function to log, like JavaScripts `console.log`, EllinghamScript
uses backticks.

```
a = 5;
`This is a logged message`
b = 10;
```

In verbose mode, every time use backticks it will also log the exact state of the script (variables,
pointer location, etc).  Note that verbose mode can end up outputting a lot of data so use it only if you need to.

### Comments
EllinghamScript supports the three common ways to add comments:
- Single comments with //
- Single comments with #
- Block comments with /* ... */

Comments are stripped out before parsing, so as you'd expect they do not affect the execution process in any way.