using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace IceDog.NetCoreMini.Core.Feature
{
    interface IHttpRequestFeature
    {

        Uri Url { get; }
        NameValueCollection Headers { get; }
        Stream Body { get; }
    }
}
