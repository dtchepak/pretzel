using System;
using System.Collections.Generic;

namespace Pretzel.Logic
{
    public class BakeOperation
    {
        private readonly Func<Oven> ovenFactory;

        public BakeOperation(Func<Oven> ovenFactory)
        {
            this.ovenFactory = ovenFactory;
            Steps = new IBakingStep[] { new ReadStep(), new GenerateStep(), new WriteStep() };
        }

        public IEnumerable<IBakingStep> Steps { get; set; }

        public void Bake(string path, string engine)
        {
            var oven = ovenFactory();
            foreach (var bakingStep in Steps)
            {
                bakingStep.BakeUsing(oven);
            }
        }
    }
}