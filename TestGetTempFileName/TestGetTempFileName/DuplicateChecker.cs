using System.Collections.Generic;
using System.Linq;

namespace TempFileNameTester
{
    public static class DuplicateChecker
    {
        public static List<string> FindDuplicates(List<string> values)
        {
            HashSet<string> set = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);
            HashSet<string> duplicates = new HashSet<string>(System.StringComparer.OrdinalIgnoreCase);

            foreach (string value in values)
            {
                if (!set.Add(value))
                {
                    duplicates.Add(value);
                }
            }

            return duplicates.ToList();
        }

        public static List<string> FindDuplicatesFromRecords(List<TempFileRecord> records)
        {
            List<string> names = new List<string>();

            foreach (TempFileRecord record in records)
            {
                names.Add(record.FileName);
            }

            return FindDuplicates(names);
        }
    }
}