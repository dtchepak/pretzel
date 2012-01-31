namespace Pretzel.Logic
{
    public interface IBakingStep
    {
        void BakeUsing(Oven context);
    }

    public class ReadStep : IBakingStep {
        public void BakeUsing(Oven context) { }
    }

    public class GenerateStep : IBakingStep {
        public void BakeUsing(Oven context) { }
    }

    public class WriteStep : IBakingStep {
        public void BakeUsing(Oven context) { }
    }
}