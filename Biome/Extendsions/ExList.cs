namespace Biome.Extendsions;

public static class ExList {

    public static T UpperBound<T>(this List<T> pDatas, T pTarget) 
        where T : IComparable<T> =>
        UpperBound(pDatas, pTarget, data => data);
    public static TRow UpperBound<TRow, TData>(this List<TRow> pDatas, TData pTarget, Func<TRow, TData> pTranslate) 
        where TData : IComparable<TData> {

        var start = 0;
        var end = pDatas.Count - 1;
        
        while (start < end) {
            var middle = (start + end) / 2;
            var middleElement = pTranslate(pDatas[middle]);
            var comparison = middleElement.CompareTo(pTarget);
            if (comparison == 0)
                return pDatas[middle];
            if (comparison > 0) {
                end = middle;
                continue;    
            }
            start = middle + 1;
        }

        return pDatas[start];
    }
}