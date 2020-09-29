using System;
using System.Collections.Generic;
using System.IO;

namespace Message.Service
{
    public class MessageService : IMessageService
    {
        /*For the sake of this test, I will use a text file as database*/
        private readonly string _dataFilePath;

        public MessageService(string dataFilePath)
        {
            if (string.IsNullOrWhiteSpace(dataFilePath))
            {
                throw new Exception("Data File path not provided");
            }

            if (!Path.HasExtension(dataFilePath) || !Path.GetExtension(dataFilePath).EndsWith("txt"))
            {
                throw new Exception("Wrong file path provided, must be .txt file");
            }

            this._dataFilePath = dataFilePath;

        }
        public List<string> GetList()
        {
            return this.MessageData;
        }

        public void Send(string message)
        {
            this.ProcessMessage(message);
        }
        private List<string> MessageData
        {
            get
            {
                try
                {
                    if (!File.Exists(_dataFilePath)) return new List<string>();
                    var data = File.ReadAllLines(this._dataFilePath);
                    return new List<string>(data);
                }
                catch (Exception)
                {
                    /*in case an issue happens*/
                    throw;
                }
            }

        }

        private void ProcessMessage(string message)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(message)) return;

                if (!File.Exists(_dataFilePath))
                {
                    using (StreamWriter sw = File.CreateText(this._dataFilePath))
                    {
                        sw.WriteLine(message);
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(this._dataFilePath))
                    {
                        sw.WriteLine(message);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
