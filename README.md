# Jasmine Headless Webkit .NET
=======

A runner for [Jasmine](https://github.com/pivotal/jasmine), inspired on [Jasmine Headless Webkit](http://johnbintz.github.com/jasmine-headless-webkit/), a Ruby Gem. This will run on any machine that can run .NET. Will work with [CoffeeScript](http://coffeescript.org/) and JavaScript.

## Documentation

Still has to be created, and will be hosted on the github wiki.

## Usage

It may be used without any parameters and a configuration file located on ScriptTests\Support\Jasmine.js relative to where the executable is being called from, like this:
	
	jasmine-headless-webkit-dotnet

You may use JavaScript or CoffeeScript when you use the runner without any parameters. Please see the [example test file](https://github.com/giggio/jasmine-headless-webkit-dotnet/blob/master/src/Tests/JasmineTests/ScriptTests/Support/Jasmine.js) for better understanding.

You may supply an html file to be tested against like this:
	
	jasmine-headless-webkit-dotnet /Filename mytestfile.html

You may also supply only the test files and source files, using JavaScript or CoffeeScript:

	jasmine-headless-webkit-dotnet /TestFiles spec1.coffee /SourceFiles source1.coffee

This is the help output:

	Jasmine Runner based on .NET
	<command> [/Help] [/FileName] [/Timeout] [/VerbosityLevel] [/TestFiles] [/SourceFiles]

	[/Help|/H]               This help text.
	[/FileName|/F]           The test archive.
	[/Timeout|/Ti]           The time to wait for the tests to complete before stopping.
	[/VerbosityLevel|/V]     The verbosity level. Defaults to normal. Possible values: normal, verbose.
	[/TestFiles|/Te]         Javascript files to test.
	[/SourceFiles|/S]        Javascript source files.

## Install via Nuget:

    Install-Package jasmine-headless-webkit-dotnet -Pre

The package can be found here: [http://nuget.org/packages/jasmine-headless-webkit-dotnet](http://nuget.org/packages/jasmine-headless-webkit-dotnet)

## Support

* View the project backlog at Github: [https://github.com/giggio/jasmine-headless-webkit-dotnet/issues](https://github.com/giggio/jasmine-headless-webkit-dotnet/issues)

## Possible issues

* Not yet tested on Mono

## Maintainers

* [Giovanni Bassi](http://blog.lambda3.com.br/L3/giovannibassi/), [Lambda3](http://www.lambda3.com.br), [@giovannibassi](http://twitter.com/giovannibassi)

This software is open source. The specific license is still to be decided. 