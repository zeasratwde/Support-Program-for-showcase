using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Mime;
using System.Threading.Tasks;
using System.IO;
using ExtractImage;


namespace ExtractImage
{
    class Program
    {
        static  void  Main(string[] args)
        {

			WebClient myWebClient = new WebClient();

			string uri = "http://www.statesymbolsusa.org/categories/state-flag";
			// read the page

			string html = new WebClient().DownloadString(uri);
			int fileSize = html.Length;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            // find the image uri
            int start = html.IndexOf("views-field-field-image",0);    // this is where the div for the flag image beigns
            int startOfNewTarget,endOfNewTarget;
			string fileName="", localFileName="";

			LocalFile localFile = new LocalFile(); 

			do
			{
				startOfNewTarget = html.IndexOf("src=", start) + 5;	 // skip 'src="'
				endOfNewTarget = html.IndexOf("?", startOfNewTarget);   // this is the end of the remote fileName

                int remoteFileNameLength = endOfNewTarget - startOfNewTarget;
                // extract the remote file name 
                string remoteFileName = html.Substring(startOfNewTarget, remoteFileNameLength);
                
                localFileName = localFile.CreateLocalFileName(remoteFileName);
                Console.WriteLine(localFileName);
                if (!localFileName.Equals("End"))
				fileName = @"C:\Users\Asrat\Desktop\StatesData\" + localFileName;


				 using  (WebClient client = new WebClient())
                {

					 if(!File.Exists(fileName))
                       client.DownloadFile(remoteFileName,fileName);
                }

                start = endOfNewTarget; // advance the the pointer
            } while (!localFileName.Equals("WY_flag.jpg"));   // the last state 


        }

        
    
    }
}