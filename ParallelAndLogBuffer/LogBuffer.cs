using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Lab4
{
    class LogBuffer : IDisposable
    {
        private volatile List<string> list;
        private string fileName;
        private Timer timer;
        private object listLock = new object();
        private object fileLock = new object();

        public LogBuffer(string fileName)
        {
            this.list = new List<string>();
            this.fileName = fileName;
            this.timer = new Timer(new TimerCallback(WriteToFileCauseTimeOut), null, 1000, 2000);
        }

        private void WriteToFileCauseTimeOut(object state)
        {
            //if (this.timer != null)
            {
                WriteToFile(state);
                Console.WriteLine("Messages are added to file cause time out");
            }
        }

        public void Dispose()
        {
            WriteToFile(null);
            Console.WriteLine("Messages are added to file cause disposing LogBuffer");
            this.timer.Dispose();
            //this.timer = null;
            
        }
        private void WriteToFile(object state)
        {
            List<string> bufferedList = new List<string>();

            lock (listLock)
            {
                bufferedList.AddRange(list);
                list.Clear();
            }

            lock (fileLock)
            {
                using (StreamWriter streamWriter = File.AppendText(this.fileName))
                {
                    foreach (string message in bufferedList)
                    {
                        streamWriter.WriteLine(message);
                    }
                    streamWriter.Flush();
                }
            }
            
        }

        public void Add(String item)
        {
            lock (listLock)
            {
                list.Add(item);
            }

            if (list.Count > 8)
            {
                WriteToFile(null);
                Console.WriteLine("Messages are added to file cause the limit is reached");
            }
        }
    }
}
