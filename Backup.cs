using System.IO.Compression;
using System.Text.RegularExpressions;

namespace BackupLib
{
    public class Backup
    {
        private string backupPath;
        private string resultPath;
        private DayOfWeek backupDayOfWeek;
        private TimeSpan backupTimeOfDay;

        public Backup(string path, string result, TimeSpan timeOfDay, DayOfWeek dayOfWeek)
        {
            backupPath = path;
            backupTimeOfDay = timeOfDay;
            resultPath = result;
            backupDayOfWeek = dayOfWeek;
        }
        public static void BackupHandler(params Backup[] backups)
        {
            for(; ; )
            {
                for(int i = 0; i < backups.Length; i++)
                {
                    Backup backup = backups[i];
                    var dateNow = DateTime.Now.TimeOfDay;
                    var dateDayOfWeek = DateTime.Now.DayOfWeek;
                    if (backup.backupDayOfWeek == dateDayOfWeek)
                    {
                        bool isEquals = EqualsUpToSeconds(backup.backupTimeOfDay, dateNow);
                        if (isEquals == true)
                        {
                            ZipFile.CreateFromDirectory(backup.backupPath, backup.resultPath + $"\\{DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss")}.zip");
                        }
                    }
                }
             Thread.Sleep(1000);
            }
        }
        public static bool EqualsUpToSeconds(TimeSpan dt1, TimeSpan dt2)
        {
            return dt1.Hours == dt2.Hours && dt1.Minutes == dt2.Minutes && dt1.Seconds == dt2.Seconds;
        }

        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input, string replacement)
        {
            return sWhitespace.Replace(input, replacement);
        }
    }
}