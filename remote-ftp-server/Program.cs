using ByteSizeLib;
using FluentFTP;
using RemoteFTPServer.Constants;
using System;
using System.Threading.Tasks;
using static RemoteFTPServer.Constants.FTPServerResource;

namespace RemoteFTPServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await ConnectFTPSAsync();
        }



        public static async Task ConnectFTPSAsync()
        {
            var conn = new FtpClient(HOST, USERNAME, PASSWORD);
            await conn.ConnectAsync();

            // get the list of files and directories in the "/download" folder
            foreach (var item in await conn.GetListingAsync(FTPServerResource.FOLDER))
            {

                switch (item.Type)
                {

                    case FtpFileSystemObjectType.Directory:

                        Console.WriteLine("Directory !  " + item.FullName);
                        Console.WriteLine("Modified date:  " + await conn.GetModifiedTimeAsync(item.FullName));

                        break;

                    case FtpFileSystemObjectType.File:

                        Console.WriteLine("File!  " + item.FullName);
                        Console.WriteLine("File size:  " +  ByteSize.FromBytes(item.Size).MegaBytes.ToString() + " Mo");
                        Console.WriteLine("Modified date:  " + await conn.GetModifiedTimeAsync(item.FullName));

                        break;

                    case FtpFileSystemObjectType.Link:
                        break;
                }
            }
        }
    }


   
}
