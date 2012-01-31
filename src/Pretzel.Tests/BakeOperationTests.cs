using System;
using System.Collections.Generic;
using System.Linq;
using Pretzel.Logic;
using Xunit;

namespace Pretzel.Tests
{
    public class BakeOperationTests
    {
        public class For_default_bake_operation : SpecificationFor<BakeOperation>
        {
            public override BakeOperation Given() { return new BakeOperation(() => null); }

            public override void When() { /* Created */ }

            [Fact]
            public void Has_read_generate_and_write_steps()
            {
                Assert.Equal(new[] { typeof(ReadStep), typeof(GenerateStep), typeof(WriteStep) }, Subject.Steps.Select(x => x.GetType()));
            }
        }

        public class For_bake_operation : SpecificationFor<BakeOperation>
        {
            const string TestPath = @"C:\test\site";
            const string TestEngine = "an awesome one";
            readonly FakeStep[] bakingSteps = new[] { new FakeStep(0), new FakeStep(1), new FakeStep(2) };
            readonly FakeOven oven = new FakeOven();

            public override BakeOperation Given()
            {
                return new BakeOperation(() => oven) {Steps = bakingSteps};
            }

            public override void When()
            {
                Subject.Bake(TestPath, TestEngine);
            }

            [Fact]
            public void Bake_site_using_all_steps_in_order()
            {
                Assert.Equal(new[] { 0, 1, 2 }, oven.StepsBaked.ToArray());
            }

            private class FakeStep : IBakingStep
            {
                private readonly int number;
                public FakeStep(int number) { this.number = number; }
                public void BakeUsing(Oven context)
                {
                    ((FakeOven)context).StepsBaked.Add(number);
                }
            }

            private class FakeOven : Oven
            {
                public readonly IList<int> StepsBaked = new List<int>();
            }
        }
    }
}