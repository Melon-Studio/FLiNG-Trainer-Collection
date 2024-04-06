using Downloader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FLiNG_Trainer.core.downloader;

public class DownloaderConfig : DownloadConfiguration
{
    public DownloaderConfig() 
    {
        WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
        webHeaderCollection.Add("Content-Type", "application/zip");

        base.ChunkCount = 1;
        base.ParallelDownload = true;
        base.MaximumBytesPerSecond = 0;
        base.RequestConfiguration.KeepAlive = true;
        base.RequestConfiguration.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
        base.RequestConfiguration.Headers = webHeaderCollection;
    }

    public DownloaderConfig(string proxy)
    {
        WebProxy webProxy = new WebProxy();
        webProxy.Address = new Uri(proxy);
        webProxy.UseDefaultCredentials = false;
        webProxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
        webProxy.BypassProxyOnLocal = true;
        WebHeaderCollection webHeaderCollection = new WebHeaderCollection();
        webHeaderCollection.Add("Content-Type", "application/zip");

        base.ChunkCount = 1;
        base.ParallelDownload = true;
        base.MaximumBytesPerSecond = 0;
        base.RequestConfiguration.KeepAlive = true;
        base.RequestConfiguration.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";
        base.RequestConfiguration.Headers = webHeaderCollection;
        base.RequestConfiguration.Proxy = webProxy;
    }
}
