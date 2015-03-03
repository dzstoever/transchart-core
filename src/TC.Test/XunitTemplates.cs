/*
using FluentAssertions;
using Xbehave;
using Xunit;
using Xunit.Extensions;

// https://xunit.codeplex.com/wikipage?title=Comparisons
[Scenario]                  is from Xbehave (also allows data-driven test attributes)        
[Fact(Timeout=n)]	        n is in milliseconds.
[Trait("name", "value")]	arbitrary metadata on a test
[Theory], [XxxData]         (data-driven test) - decorate with one or more [XxxData] attributes, including 
                            [InlineData], [PropertyData], [SqlServerData], [OleDbData], [ExcelData]


namespace Zen.Test
{
     
    public class XSimpleScenario
    {
        [Scenario]
        public virtual void DoSomething()
        {
            int? x = null;
            "Given x=1 ".Given(() => x = 1);
            "When adding 1 ".When(() => x += 1);
            "Then the result is 2".Then(() => x.Should().Be(2));
        }
    }

    public class XScenarios : UseLogFixture
    {
        [Scenario]
        public virtual void DoSomething()
        {
            Console.WriteLine("I am not in the output");

            "Given some arranged preconditions".Given(() =>
            {
                Console.WriteLine("-arrange-");
                Log.InfoFormat("given something");

            });
            "When the code under test runs".When(() =>
            {
                Console.WriteLine("-act-");
                Log.InfoFormat("do something with it");

            });
            "Then the actual results meet expectations".Then(() =>
            {
                Console.WriteLine("-assert-");
                Log.InfoFormat("is everything as it should be");
            });            
        }

        ~XScenarios()
        {
            //cleanup, dispose
        }
    }

}
*/