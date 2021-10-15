namespace GameStore.Foundation
{
    public abstract class Mapper<TSource, TTarget> 
        : IMapper<TSource, TTarget>
        where TSource: class
        where TTarget: class, new()
    {
        public abstract void Map(TSource source, TTarget target);

        public TTarget Map(TSource source)
        {
            source.ShouldNotNull();

            var target = new TTarget();

            Map(source, target);

            return target;
        }
    }
}