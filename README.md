# JSON FORMATTER
## About
This simple library was created to format output string of .NET Framework System.Web.Script.Serialization.JavaScriptSerializer.Serialize single-line output.
JSON files can be used to store app settings, and modifying or inspecting few hundred long, single-line text is far from comfortable.
JsonFormatter breaks single-line input into lines and indents nested elements.
## Use
JsonFormatter consists of one method - FormatJson, which takes a string as an argument. Input string should be single-line output of a serialization method, such as System.Web.Script.Serialization.JavaScriptSerializer.Serialize.