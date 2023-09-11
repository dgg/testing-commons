
# What
**Testing.Commons** contains classes and extensions that help testing code in general and more specific areas such as configuration and web artifacts.

[![testing.commons build](https://github.com/dgg/testing-commons/actions/workflows/build.yml/badge.svg)](https://github.com/dgg/testing-commons/actions/workflows/build.yml)

![Nuget Testing.Commons](https://img.shields.io/nuget/v/testing.commons?logo=nuget&label=Testing.Commons)<br/>
![Nuget Testing.Commons.NUnit](https://img.shields.io/nuget/v/testing.commons.nunit?logo=nuget&label=Testing.Commons.NUnit)


![GitHub tag Testing.Commons)](https://img.shields.io/github/v/tag/dgg/testing-commons?logo=github&filter=v*&label=Testing.Commons)<br/>
![GitHub tag Testing.Commons.NUnit)](https://img.shields.io/github/v/tag/dgg/testing-commons?logo=github&filter=nunit*&label=Testing.Commons.NUnit)


# Why
For some time now I have been testing code from different applications using different testing frameworks.
Testing frameworks are awesome (some more than others) but they cannot cover all cases I found and sometimes they should not even try.

When some help to perform certain tasks was needed helpers, techniques and extensions have been developed or found and borrowed. I want to share some of those and maybe people can contribute with their own useful trickery.

# How
**Testing.Commons** (the project) is made up of three (so far) differentiated libraries

## Testing.Commons
**_Testing.Commons_** (the library) contains shared artifacts that can be applied to any testing framework. It contains no external dependencies outside the .NET Framework itself.

## Testing.Commons.NUnit
Most of the work that has given born to this project has been developed using [NUnit](http://www.nunit.com).
NUnit offers multiple extensibility points of which I have taken advantage to extend what the framework can do for me.
This library will contain NUnit-dependant code, mostly custom constraints.

## Testing.Commons.ServiceStack
> **DEPRECATED** <br/>
> Having moved to target .NET 6 (previously .NET Core), _Testing.Commons.ServiceStack_ has been deprecated and __will not__ be receiving further improvements.

~~[ServiceStack](https://github.com/ServiceStackV3/ServiceStackV3) has turned into my default weapon of choice when developing services.~~

~~Although it does not receive the focus on testing like other frameworks (such as [NancyFx](http://nancyfx.org/)),
with very little extra work, you can test your services end to end (should you want to do so) or
test a single feature of your services in an integrated manner.~~

~~__Testing.Commons.ServiceStack__ provides that extra work, so that you only focus in what is important:
the correctness of your services.~~
