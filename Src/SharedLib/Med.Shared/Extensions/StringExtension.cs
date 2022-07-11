namespace Med.Shared.Extensions
{
    public static class StringExtension
    {
        public static List<int> SplitToIntList(this string list, char separator = ',')
        {
            int result = 0;
            return (from s in list.Split(',')
                    let isint = int.TryParse(s, out result)
                    let val = result
                    where isint
                    select val).ToList();
        }
    }
}
