using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace QuotesService.Class
{
    public interface IAccessOutlook
    {
        void SignatureRawText(bool isStart, bool newTemplate);
        bool IsOutlookRunning();
    }

    public class AccessOutlook : IAccessOutlook
    {
        public string signatureText { get; set; }
        public string applicationDataDir { get; set; }
        public string fileName { get; set; }
        public string signature { get; set; }
        public int index { get; set; }
        public string applicationDir { get; set; }


        public void SignatureRawText(bool isStart, bool newTemplate)
        {
            applicationDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Microsoft\\Signatures";
            signature = string.Empty;
            DirectoryInfo diInfo = new DirectoryInfo(applicationDataDir);
            if (diInfo.Exists)
            {
                FileInfo[] fiSignature = diInfo.GetFiles("*.htm");
                if (fiSignature.Length > 0)
                {
                    applicationDir = Environment.CurrentDirectory;
                    fileName = fiSignature[0].Name.Replace(fiSignature[0].Extension, string.Empty);

                    if (File.Exists(applicationDir + @"\template.htm") && newTemplate)
                    {
                        File.Delete(applicationDir + @"\template.htm");
                    }

                    if (File.Exists(applicationDir + @"\template.htm"))
                    {
                        diInfo = null;
                        diInfo = new DirectoryInfo(applicationDir);
                        fiSignature = diInfo.GetFiles("*.htm");
                        StreamReader streamReader = new StreamReader(fiSignature[0].FullName, Encoding.Default);
                        signature = streamReader.ReadToEnd();
                        streamReader.Close();
                    }
                    else
                    {
                        File.Copy(fiSignature[0].FullName, applicationDir + "/template.htm");
                    }
                    if (!string.IsNullOrEmpty(signature))
                    {
                        try
                        {
                            var quotesSignature = new QuotesSignature();

                            string strQuotes = quotesSignature.GetQuote();
                            if (isStart)
                            {
                                signatureText = signature.Replace("#quotes#", strQuotes);


                                byte[] seeds = Encoding.ASCII.GetBytes(signatureText);
                                var fileStream = new FileStream(applicationDataDir + "/" + fileName + ".htm", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                                var binaryWriter = new BinaryWriter(fileStream);
                                if (fileStream.CanWrite)
                                {
                                    for (int i = 0; i < seeds.Length; i++)
                                    {
                                        if ((seeds[i]) != Convert.ToByte('?'))
                                        {
                                            binaryWriter.Write(seeds[i]);
                                        }
                                    }
                                    binaryWriter.Flush();
                                    binaryWriter.Close();
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            throw exception;
                        }

                    }
                }
            }


        }

        public bool IsOutlookRunning()
        {
            bool retVal = false;
            try
            {
                foreach (Process availableProcess in Process.GetProcesses("."))
                {
                    if (availableProcess.MainWindowTitle.Length > 0)
                    {
                        if (availableProcess.ProcessName == "OUTLOOK")
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                retVal = false;
                throw exception;
            }

            return retVal;
        }
    }
}
