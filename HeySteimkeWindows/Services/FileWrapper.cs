using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HeySteimke.Services
{
    class FileWrapper
    {
        public Task<string> ReadAll(string file)
        {
            return Task.Run(() =>
            {
                string content = "";
                bool readFile = false;
                for (int cnt = 0; cnt < 100 && !readFile; cnt++)
                {
                    try
                    {
                        content = File.ReadAllText(file);
                        readFile = true;
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine("Exception while read profile");
                        Debug.WriteLine(exc);
                    }
                }
                if (!readFile)
                {
                    Debug.WriteLine("Could not Read File");
                }
                return content;
            });
        }

        public Task WriteAll(string file,string text)
        {
            return Task.Run(() =>
            {
                bool wroteData = false;
                for (int cnt = 0; cnt < 100 && !wroteData; cnt++)
                {
                    try
                    {
                        if (!File.Exists(file))
                            File.Create(file);
                        File.WriteAllText(file, text);
                        wroteData = true;
                    }
                    catch (Exception exc)
                    {
                        Debug.WriteLine("Exception while write profile");
                        Debug.WriteLine(exc);
                    }
                }
                if(wroteData == false)
                {
                    Debug.WriteLine("Could not Write File");
                }
            });
        }

        public Task<bool> ExistsAsync(string file)
        {
            return Task.Run(() =>
            {
                return File.Exists(file);
            });
        }

        public string GetLocalAppDataFile(string name)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), name);
        }
    }
}
