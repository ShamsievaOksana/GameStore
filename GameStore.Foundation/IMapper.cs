using System.Runtime.InteropServices;

namespace GameStore.Foundation
{
    public interface IMapper<in TSource, TTarget>
    {
        void Map(TSource source, TTarget target);

        TTarget Map(TSource source);
    }
}